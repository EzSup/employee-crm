using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace HRM_TgBot.Core.Models;

public class RequestData
{
    public string Message { get; set; }
    public Func<Message, ITelegramBotClient?, Task<bool>> TryApply { get; set; }
    public IReplyMarkup? Keyboard { get; set; }
}