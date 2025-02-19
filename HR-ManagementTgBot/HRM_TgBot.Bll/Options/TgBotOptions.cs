using Microsoft.Extensions.Configuration;

namespace HRM_TgBot.Bll.Options;

public class TgBotOptions
{
    [ConfigurationKeyName("BotToken")] public string BotToken { get; set; }
}