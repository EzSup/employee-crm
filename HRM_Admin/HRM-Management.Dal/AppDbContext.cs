using HRM_Management.Dal.Configurations;
using HRM_Management.Dal.Entities;
using HRM_Management.Dal.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HRM_Management.Dal
{
    public class AppDbContext : IdentityDbContext<UserEntity, IdentityRole<int>, int>
    {
        private readonly string _connectionString;

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options, IOptions<DatabaseOptions> dbSettings) : base(options)
        {
            _connectionString = dbSettings.Value.ConnectionString;
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PersonEntity> Persons { get; set; }
        public DbSet<HubEntity> Hubs { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<ExEmployeeEntity> ExEmployees { get; set; }
        public DbSet<ChildEntity> Children { get; set; }
        public DbSet<PartnerEntity> Partners { get; set; }
        public DbSet<PersonTranslateEntity> Translates { get; set; }
        public DbSet<BlogEntity> Blogs { get; set; }
        public DbSet<SubscriptionEntity> Subscriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PersonConfiguration());
            builder.ApplyConfiguration(new HubConfiguration());
            builder.ApplyConfiguration(new EmployeeConfiguration());
            builder.ApplyConfiguration(new ExEmployeeConfiguration());
            builder.ApplyConfiguration(new ChildConfiguration());
            builder.ApplyConfiguration(new PartnerConfiguration());
            builder.ApplyConfiguration(new PersonsTranslateConfiguration());
            builder.ApplyConfiguration(new SubscriptionConfiguration());
            builder.ApplyConfiguration(new BlogConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
