using HRM_Management.Infrastructure.Quartz.Jobs;
using Microsoft.Extensions.Options;
using Quartz;

namespace HRM_Management.Infrastructure.Quartz.Setups
{
    public class ChildrenBirthdayNotificationSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            var jobKey = JobKey.Create(nameof(ChildrenBirthdayNotificationJob));
            options.AddJob<ChildrenBirthdayNotificationJob>(JobBuilder => JobBuilder.WithIdentity(jobKey).StoreDurably());
        }
    }
}