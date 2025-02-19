using Microsoft.Extensions.Configuration;
namespace HRM_TgBot.Bll.Options
{
    public class RabbitMQOptions
    {
        [ConfigurationKeyName("Host")] public string Host { get; set; }
        [ConfigurationKeyName("Port")] public int Port { get; set; }
        [ConfigurationKeyName("ExchangeName")] public string ExchangeName { get; set; }
        [ConfigurationKeyName("QueueName")] public string QueueName { get; set; }
        [ConfigurationKeyName("RoutingKey")] public string RoutingKey { get; set; }
        [ConfigurationKeyName("Username")] public string Username { get; set; }
        [ConfigurationKeyName("Password")] public string Password { get; set; }
    }
}
