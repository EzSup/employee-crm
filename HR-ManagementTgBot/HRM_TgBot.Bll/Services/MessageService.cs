using HRM_TgBot.Core.ServicesInterfaces;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace HRM_TgBot.Bll.Services;

public class MessageService : IMessageService
{
    private readonly ITelegramBotClient _botClient;

    public MessageService(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public async Task SendTextMessageAsync(string text, string chatId)
    {
        await _botClient.SendTextMessageAsync(chatId, text, parseMode: ParseMode.Html);
    }
}