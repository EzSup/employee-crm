using Microsoft.Extensions.Configuration;

namespace HRM_Management.Infrastructure.Options
{
    public class EmailConfiguration
    {
        [ConfigurationKeyName("From")] public string From { get; set; }
        [ConfigurationKeyName("SmtpServer")] public string SmtpServer { get; set; }
        [ConfigurationKeyName("Port")] public int Port { get; set; }
        [ConfigurationKeyName("Password")] public string Password { get; set; }
    }
}
