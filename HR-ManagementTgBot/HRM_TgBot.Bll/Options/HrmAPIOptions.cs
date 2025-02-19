using Microsoft.Extensions.Configuration;

namespace HRM_TgBot.Bll.Options;

public class HrmAPIOptions
{
    [ConfigurationKeyName("BaseUrl")] public string BaseUrl { get; set; }
}