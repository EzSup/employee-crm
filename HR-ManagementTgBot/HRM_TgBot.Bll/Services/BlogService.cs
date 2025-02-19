using System.Collections.Concurrent;
using System.ComponentModel;
using System.Text.Json;
using HeyRed.Mime;
using HRM_TgBot.Core.Models;
using HRM_TgBot.Core.ServicesInterfaces;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using static HRM_TgBot.Core.Helpers.Constants.AppConstants;

namespace HRM_TgBot.Bll.Services;

public class BlogService : IBlogService
{
    private readonly ConcurrentDictionary<string, BlogFile> _fileCash;
    private readonly HttpClient _httpClient;
    private readonly ILogger<BlogService> _logger;
    private List<Blog> _blogList;

    public BlogService(IHttpClientFactory httpClientFactory, ILogger<BlogService> logger)
    {
        _httpClient = httpClientFactory.CreateClient(BLOG_HTTP_CLIENT_NAME);
        _fileCash = new ConcurrentDictionary<string, BlogFile>();
        _blogList = new List<Blog>();
        _logger = logger;
        HandleBlogSynchronizationAsync().Wait();
    }

    public async Task SendDocumentAsync(ITelegramBotClient telegramBotClient, long userID, string blogTitle)
    {
        var blogFile = GetFile(blogTitle);
        await telegramBotClient.SendDocumentAsync(
            userID,
            InputFile.FromStream(blogFile.FileStream, blogFile.FullFileName)
        );
    }

    public async Task HandleBlogSynchronizationAsync(List<Blog>? blogs = null)
    {
        _blogList = blogs ?? await LoadBlogsAsync();
        await DownloadFileAsync();
    }

    public ReplyKeyboardMarkup GetBlogButtons()
    {
        var titles = _blogList.Select(b => b.Title).ToList();

        var buttons = titles.Select(t => new[]
        {
            new KeyboardButton(t)
        }.ToArray());
        return new ReplyKeyboardMarkup(buttons);
    }

    public async Task<List<Blog>> LoadBlogsAsync()
    {
        var response = await _httpClient.GetAsync("all");
        var responseMessage = response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();

        var blogs = JsonSerializer
            .Deserialize<List<Blog>>(jsonString,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        return blogs ?? throw new WarningException("there is no blogs received");
    }

    private async Task DownloadFileAsync()
    {
        foreach (var blog in _blogList)
        {
            var response = await _httpClient.GetAsync(blog.ContentLink);
            response.EnsureSuccessStatusCode();

            var mimeExtension = response.Content.Headers.ContentType?.MediaType;
            var fileExtension = MimeTypesMap.GetExtension(mimeExtension) ?? ".bin";

            var memoryStream = new MemoryStream();
            await response.Content.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            _fileCash.TryAdd($"{blog.Title}", new BlogFile($"{blog.Title}.{fileExtension}", memoryStream));
        }
    }

    private BlogFile GetFile(string title)
    {
        if (_fileCash.TryGetValue(title, out var blogFile)) return blogFile;
        throw new FileNotFoundException(title);
    }
}