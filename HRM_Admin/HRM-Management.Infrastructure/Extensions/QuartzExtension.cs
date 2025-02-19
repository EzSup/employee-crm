using HRM_Management.Core.Services;
using HRM_Management.Infrastructure.Quartz.Setups;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
namespace HRM_Management.Infrastructure.Extensions
{
    public static class QuartzExtension
    {
        public static async Task AddQuartzConfiguration(this IServiceCollection services)
        {
            services.AddQuartz(options =>
            {
                options.UseMicrosoftDependencyInjectionJobFactory();
                options.UseDefaultThreadPool(x => x.MaxConcurrency = 5);
                options.MisfireThreshold = new TimeSpan(0, 0, 10);
                options.UsePersistentStore(storeOptions =>
                {
                    storeOptions.UsePostgres(postgresOptions =>
                    {
                        postgresOptions.ConnectionStringName = "DefaultConnection";
                        postgresOptions.TablePrefix = "qrtz_";
                    });
                    storeOptions.PerformSchemaValidation = true;
                    storeOptions.UseClustering();
                    storeOptions.UseSystemTextJsonSerializer();
                });
            });
            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });

            services.ConfigureOptions<ChildrenBirthdayNotificationSetup>();
            services.ConfigureOptions<EmployeeBirthdayCongratulationSetup>();
            services.ConfigureOptions<EmployeeWelcomeAnnouncementSetup>();
            
            var schedulerService = services.BuildServiceProvider()
                                           .GetRequiredKeyedService<ISchedulerService>(null);
            await schedulerService.InitializeSchedulerAsync();
        }
    }
}