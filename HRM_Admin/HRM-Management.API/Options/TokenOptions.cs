namespace HRM_Management.API.Options
{
    public class TokenOptions
    {
        [ConfigurationKeyName("ExpiresDays")] public double ExpiresDays { get; set; }
        [ConfigurationKeyName("Issuer")] public string? Issuer { get; set; }
    }
}
