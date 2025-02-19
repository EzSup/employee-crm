using HRM_TgBot.Core.Models;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace HRM_TgBot.Bll.TgBotMessageHandlers;

public static class StartMessageHandler
{
    public static async Task HandleStartMessageAsync(ITelegramBotClient botClient, CancellationToken cancellationToken,
        BotUser user)
    {
        var buttuns =
            new List<KeyboardButton>
            {
                new("Корисні посилання")
            };

        if (!user.Verified) buttuns.Add(new KeyboardButton("Заповнити анкету"));

        var replyKeyboard = new ReplyKeyboardMarkup(buttuns);

        await botClient.SendTextMessageAsync(
            user.UserTgID,
            "Вас вітає помічник по інтеграції персоналу \n виберіть одну із опцій",
            cancellationToken: cancellationToken, replyMarkup: replyKeyboard);
    }
}