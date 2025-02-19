using Microsoft.Extensions.Configuration;

namespace HRM_Management.Dal.Options
{
    public class DatabaseOptions
    {
        [ConfigurationKeyName("DefaultConnection")] public string ConnectionString { get; set; }
    }
}
