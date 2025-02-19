using HRM_TgBot.Core.Models;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace HRM_TgBot.Core.ServicesInterfaces;

public interface IBlogService
{
    Task SendDocumentAsync(ITelegramBotClient telegramBotClient, long userID, string blogTitle);
    Task HandleBlogSynchronizationAsync(List<Blog>? blogs = null);
    ReplyKeyboardMarkup GetBlogButtons();
    Task<List<Blog?>> LoadBlogsAsync();
}