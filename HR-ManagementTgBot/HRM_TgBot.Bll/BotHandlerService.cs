using HRM_TgBot.Bll.MessageHandlers;
using HRM_TgBot.Core.Helpers.Enums;
using HRM_TgBot.Core.ServicesInterfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using static HRM_TgBot.Bll.TgBotMessageHandlers.ApplicationFillingHandler;
using static HRM_TgBot.Bll.TgBotMessageHandlers.StartMessageHandler;
using static HRM_TgBot.Bll.TgBotMessageHandlers.UserCollectionService;
using static HRM_TgBot.Core.Helpers.Constants.QueueConstantsUK;

namespace HRM_TgBot.Bll;

public class BotHandlerService : IHostedService
{
    private readonly IBlogService _blogService;
    private readonly ITelegramBotClient _botClient;
    private readonly ErrorHandler _errorHandler;
    private readonly ILogger _logger;
    private readonly IPersonSubmissionService _personSubmissionService;
    private CancellationTokenSource _cancellationTokenSRC;

    public BotHandlerService(ITelegramBotClient botClient, IBlogService blogService,
        IPersonSubmissionService personSubmissionService,
        ILogger<BotHandlerService> logger)
    {
        _botClient = botClient;
        _logger = logger;
        _errorHandler = new ErrorHandler(logger);
        _blogService = blogService;
        _personSubmissionService = personSubmissionService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _cancellationTokenSRC = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        _botClient.StartReceiving(
            HandleUpdateMessage,
            _errorHandler.HandlePollingErrorAsync,
            new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            },
            _cancellationTokenSRC.Token
        );

        _logger.Log(LogLevel.Information, "Bot is running!!");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _cancellationTokenSRC.Cancel();
        _logger.Log(LogLevel.Information, "Bot was stopped");
        return Task.CompletedTask;
    }

    private async Task HandleUpdateMessage(ITelegramBotClient client, Update update, CancellationToken token)
    {
        if (update.Message is not { } message)
            return;
        var messageText = message.Text;

        var userId = message.From.Id;

        var user = GetUser(userId, message.From.Username);

        if (messageText == "/start")
        {
            await _personSubmissionService.CheckUserSubmissionAsync(user);
            await HandleStartMessageAsync(client, token, user);
            return;
        }

        if (messageText == FILL_IN_FORM_OPTION && user.WaitType == WaitType.WaitForFiling)
        {
            user.WaitType = WaitType.WaitValue;
            await client.SendTextMessageAsync(
                userId,
                START_FILLING_IN_FORM,
                replyMarkup: new ReplyKeyboardRemove()
            );
            try
            {
                await StartApplicationFilling(client, token, user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{messageText} from {userId} {ex.Message}");
            }

            return;
        }

        if (user != null && user.WaitType == WaitType.WaitValue)
            await ContinueApplicationFilling(client, message, user, _personSubmissionService);

        if (user?.WaitType == WaitType.WaitForBlog || messageText == BLOGS_OPTION)
        {
            user.WaitType = WaitType.WaitForBlog;
            await client.SendTextMessageAsync(
                userId,
                HERE_ARE_BLOGS,
                replyMarkup: _blogService.GetBlogButtons()
            );
            user.WaitType = WaitType.WaitForBlogFile;
            return;
        }

        if (user.WaitType == WaitType.WaitForBlogFile)
        {
            await _blogService.SendDocumentAsync(client, userId, messageText);
            user.WaitType = WaitType.WaitForBlog;
            await HandleStartMessageAsync(client, token, user);
        }
    }
}