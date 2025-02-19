using Microsoft.Extensions.Configuration;
namespace HRM_Management.Infrastructure.Options
{
    public class HRM_TgBotApiOptions
    {
        [ConfigurationKeyName("BaseUrl")] public string BaseUrl { get; set; }
        [ConfigurationKeyName("LogInKey")] public string LogInKey { get; set; }
        [ConfigurationKeyName("ExpirationTimeMinutes")] public int ExpirationTimeMinutes { get; set; }
        [ConfigurationKeyName("QueueName")] public string QueueName { get; set; }
        [ConfigurationKeyName("RoutingKey")] public string RoutingKey { get; set; }
    }
}
