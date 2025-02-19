using Microsoft.Extensions.Configuration;
namespace HRM_Management.Infrastructure.Options
{
    public class RabbitMQOptions
    {
        [ConfigurationKeyName("Host")] public string Host { get; set; }
        [ConfigurationKeyName("Port")] public int Port { get; set; }
        [ConfigurationKeyName("ExchangeName")] public string ExchangeName { get; set; }
        [ConfigurationKeyName("Username")] public string Username { get; set; }
        [ConfigurationKeyName("Password")] public string Password { get; set; }
    }
}
