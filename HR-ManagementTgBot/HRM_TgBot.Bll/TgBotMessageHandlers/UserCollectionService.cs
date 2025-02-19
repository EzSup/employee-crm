using HRM_TgBot.Core.Models;

namespace HRM_TgBot.Bll.TgBotMessageHandlers;

public static class UserCollectionService
{
    private static readonly List<BotUser> Users = new();

    public static BotUser GetUser(long userId, string? userName = null)
    {
        var user = Users.SingleOrDefault(u => u.UserTgID == userId);

        if (user is null)
        {
            user = new BotUser
            {
                UserTgID = userId,
                TgUserName = userName
            };
            Users.Add(user);
        }

        return user;
    }
}