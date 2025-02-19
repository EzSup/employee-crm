namespace HRM_Management.API.Options
{
    public class OpenTelemetryOptions
    {
        [ConfigurationKeyName("ApiKey")] public string? ApiKey { get; set; }
        [ConfigurationKeyName("BaseUrl")] public string? BaseUrl { get; set; }
    }
}
