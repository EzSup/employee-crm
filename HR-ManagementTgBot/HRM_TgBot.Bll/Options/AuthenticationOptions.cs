using Microsoft.Extensions.Configuration;

namespace HRM_TgBot.Bll.Options;

public class AuthenticationOptions
{
    [ConfigurationKeyName("Issuer")] public string Issuer { get; set; }
    [ConfigurationKeyName("Audience")] public string Audience { get; set; }
    [ConfigurationKeyName("LogInKey")] public string LogInKey { get; set; }
    [ConfigurationKeyName("SecretKey")] public string SecretKey { get; set; }

    [ConfigurationKeyName("ExpirationTimeMinutes")]
    public int ExpirationTimeMinutes { get; set; }
}