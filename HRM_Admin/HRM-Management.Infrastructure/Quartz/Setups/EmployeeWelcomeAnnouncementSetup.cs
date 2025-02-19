using HRM_Management.Infrastructure.Quartz.Jobs;
using Microsoft.Extensions.Options;
using Quartz;

namespace HRM_Management.Infrastructure.Quartz.Setups;

public class EmployeeWelcomeAnnouncementSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var jobKey = new JobKey(nameof(EmployeeWelcomeAnnouncementSetup));
        var triggerKey = new TriggerKey(jobKey.ToString());
        options.AddJob<EmployeeWelcomeAnnouncementJob>(builder =>
                builder.WithIdentity(jobKey))
            .AddTrigger(conf=>conf.WithIdentity(triggerKey)
                .ForJob(jobKey)
                .WithDailyTimeIntervalSchedule(x => x.StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(10, 0))
                .WithIntervalInMinutes(24))
            );
    }
}