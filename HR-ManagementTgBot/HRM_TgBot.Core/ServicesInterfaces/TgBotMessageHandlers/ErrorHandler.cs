using System.Net;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;

namespace HRM_TgBot.Bll.MessageHandlers;

public class ErrorHandler
{
    private readonly ILogger _logger;

    public ErrorHandler(ILogger logger)
    {
        _logger = logger;
    }

    public async Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        _logger.LogError(exception.Message, exception);
        switch (exception)
        {
            case ApiRequestException apiException:
                Console.WriteLine($"Telegram API Error:\n[{apiException.ErrorCode}] {apiException.Message}");
                if (apiException.HttpStatusCode == HttpStatusCode.TooManyRequests)
                {
                    _logger.LogError(apiException, "Занадто багато запитів, чекаємо перед повтором...");
                    await Task.Delay(1000, token);
                }

                break;
            case HttpRequestException httpRequestException:
                _logger.LogError(httpRequestException, "Network error occurred, retrying...");
                await Task.Delay(2000, token);
                break;

            default:
                _logger.LogError($"Unexpected exception: {exception.Message}");
                break;
        }
    }
}