using HRM_TgBot.Core.Models;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using static HRM_TgBot.Core.Helpers.Constants.QueueConstantsUK;

namespace HRM_TgBot.Bll.TgBotMessageHandlers;

public static class StartMessageHandler
{
    public static async Task HandleStartMessageAsync(ITelegramBotClient botClient, CancellationToken cancellationToken,
        BotUser user)
    {
        var buttuns =
            new List<KeyboardButton>
            {
                new(BLOGS_OPTION)
            };

        if (!user.Verified) buttuns.Add(new KeyboardButton(FILL_IN_FORM_OPTION));

        var replyKeyboard = new ReplyKeyboardMarkup(buttuns);

        await botClient.SendTextMessageAsync(
            user.UserTgID,
            WELCOME_MESSAGE,
            cancellationToken: cancellationToken, replyMarkup: replyKeyboard);
    }
}