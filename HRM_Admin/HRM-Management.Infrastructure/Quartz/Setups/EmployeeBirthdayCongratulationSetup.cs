using HRM_Management.Infrastructure.Quartz.Jobs;
using Microsoft.Extensions.Options;
using Quartz;
namespace HRM_Management.Infrastructure.Quartz.Setups
{
    public class EmployeeBirthdayCongratulationSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            var jobKey = JobKey.Create(nameof(EmployeeBirthdayCongratulationJob));
            var triggerKey = new TriggerKey(jobKey.ToString());
            options.AddJob<EmployeeBirthdayCongratulationJob>(JobBuilder =>
                                                                  JobBuilder.WithIdentity(jobKey))
                   .AddTrigger(conf => conf.WithIdentity(triggerKey)
                                           .ForJob(jobKey)
                                           .WithDailyTimeIntervalSchedule(x =>x.StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(9, 0))
                                            .WithIntervalInHours(24))
                   
                   );
        }
    }
}