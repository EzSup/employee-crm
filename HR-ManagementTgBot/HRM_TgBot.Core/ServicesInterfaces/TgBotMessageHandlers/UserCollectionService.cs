using HRM_TgBot.Core.Models;

namespace HRM_TgBot.Bll.TgBotMessageHandlers;

public static class UserCollectionService
{
    private static readonly List<BotUser> _users = new();

    public static BotUser GetUser(long userId, string? userName = null)
    {
        var user = _users.SingleOrDefault(u => u.UserTgID == userId);

        if (user is null)
        {
            user = new BotUser
            {
                UserTgID = userId,
                TgUserName = userName
            };
            _users.Add(user);
        }

        return user;
    }
}