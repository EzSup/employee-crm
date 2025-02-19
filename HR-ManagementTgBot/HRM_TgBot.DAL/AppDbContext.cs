using HR_ManagementTgBot.models;
using HRM_TgBot.DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HRM_TgBot.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<BotUser> Users { get; set; }

        private readonly string _connectionString;

        public AppDbContext(DbContextOptions options, IOptions<DatabaseOptions> dbsetings) : base(options)
        {
            _connectionString = dbsetings.Value.ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
