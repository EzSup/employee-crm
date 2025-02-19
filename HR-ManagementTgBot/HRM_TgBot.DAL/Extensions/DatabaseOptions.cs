using Microsoft.Extensions.Configuration;

namespace HRM_TgBot.DAL.Extensions
{
    public class DatabaseOptions
    {
        [ConfigurationKeyName("DefaultConnection")]
        public string ConnectionString { get; set; }
    }
}
