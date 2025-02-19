using HRM_TgBot.Core.Helpers.Enums;
using HRM_TgBot.Core.Models;
using HRM_TgBot.Core.ServicesInterfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace HRM_TgBot.Bll.TgBotMessageHandlers;

public static class ApplicationFillingHandler
{
    public static async Task StartApplicationFilling(ITelegramBotClient botClient, CancellationToken cancellationToken,
        BotUser user)
    {
        await botClient.SendTextMessageAsync(user.UserTgID,
            user.RequestData.Peek().Message,
            cancellationToken: cancellationToken, replyMarkup: user.RequestData.Peek().Keyboard);
    }

    public static async Task ContinueApplicationFilling(ITelegramBotClient botClient, Message message, BotUser botUser,
        IPersonSubmissionService personSubmissionService)
    {
        if (await botUser.RequestData.Peek().TryApply(message, botClient))
        {
            botUser.RequestData.Dequeue();

            if (botUser.RequestData.Count > 0)
            {
                await botClient.SendTextMessageAsync(botUser.UserTgID, botUser.RequestData.Peek().Message,
                    replyMarkup: botUser.RequestData.Peek().Keyboard);
            }
            else
            {
                await personSubmissionService.SubmitPersonAsync(botUser);
                botUser.WaitType = WaitType.ApplicationFilled;
                await botClient.SendTextMessageAsync(botUser.UserTgID, "Ви зповнили форму !",
                    replyMarkup: new ReplyKeyboardRemove());
            }
        }
        else
        {
            await botClient.SendTextMessageAsync(botUser.UserTgID, "Ведено не коректні дані");
        }
    }
}