namespace HRM_TgBot.Core.ServicesInterfaces;

public interface IMessageService
{
    Task SendTextMessageAsync(string text, string chatId);
}