using HRM_Management.API.Extensions;
using HRM_Management.API.Infrastructure;
using HRM_Management.Bll.Helpers;
using HRM_Management.Dal;
using HRM_Management.Dal.Entities;
using HRM_Management.Dal.Extensions;
using HRM_Management.Dal.Repositories;
using HRM_Management.DalS3.Extensions;
using HRM_Management.GraphQl.Extensions;
using HRM_Management.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
namespace HRM_Management.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddUserSecrets<Program>();
            var configuration = builder.Configuration;
            builder.Services.ConfigureOptions(configuration);

            builder.Services.AddS3();
            builder.Services.AddMemoryCache();

            builder.Services.AddLoggingServices(configuration);
            builder.Services.AddServices();
            builder.Services.AddAutoMapper(typeof(AppMappingProfile));
            builder.Services.AddUnitOfWork<AppDbContext>();
            builder.Services.AddCustomRepository<PersonEntity, PersonRepository>();
            builder.Services.AddCustomRepository<BlogEntity, BlogRepository>();
            builder.Services.AddCustomRepository<EmployeeEntity, EmployeeRepository>();
            builder.Services.AddCustomRepository<HubEntity, HubRepository>();

            builder.Services.AddDbContext<AppDbContext>();
            builder.Services.AddDbContextFactory<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")),
                                                               ServiceLifetime.Scoped);
            builder.Services.AddStackExchangeRedisCache(options => options.Configuration = configuration.GetConnectionString("Redis"));
            builder.Services.AddControllers();

            builder.Services.AddIdentityConfiguration(configuration);
            builder.Services.AddFluentValidationWithConfigurations();

            builder.Services.AddGraphQlConfiguration();

            builder.Services.AddInfrastructureExtensions();
            builder.Services.AddRabbitConnection(configuration);

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();
            builder.Services.ConfigureCookies();
            builder.Services.ConfigureCors();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseExceptionHandler();

            app.MapControllers();
            app.MapGraphQL();

            app.Run();
        }
    }
}
