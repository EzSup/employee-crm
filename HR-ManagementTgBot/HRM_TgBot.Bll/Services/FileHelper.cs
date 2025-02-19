using HRM_TgBot.Core.Models;
using Microsoft.AspNetCore.Http;
using Telegram.Bot;

namespace HRM_TgBot.Bll.Services;

public class FileHelper
{
    public async Task<IFormFile> GetFileFromTgAsync(ITelegramBotClient telegramBotClient, string filePath)
    {
        var memoryStream = new MemoryStream();
        await telegramBotClient.DownloadFileAsync(filePath, memoryStream);
        var fileBytes = memoryStream.ToArray();
        var file = new CustomFormFile(fileBytes, Path.GetFileName(filePath), GetFileExtension(filePath));

        return file;
    }

    private string GetFileExtension(string filePath)
    {
        var extension = Path.GetExtension(filePath).ToLower();

        var mimeType = extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".webp" => "image/webp",
            ".pdf" => "application/pdf",
            ".txt" => "text/plain",
            ".doc" or ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".xls" or ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            _ => "application/octet-stream"
        };

        return mimeType;
    }
}