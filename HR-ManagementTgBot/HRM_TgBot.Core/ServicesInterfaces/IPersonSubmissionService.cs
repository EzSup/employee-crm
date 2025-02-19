using HRM_TgBot.Core.Models;

namespace HRM_TgBot.Core.ServicesInterfaces;

public interface IPersonSubmissionService
{
    Task CheckUserSubmissionAsync(BotUser botUser);
    Task SubmitPersonAsync(BotUser user);
}