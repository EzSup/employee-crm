using HRM_Management.Core.Services;
using Quartz;
using Quartz.Simpl;

namespace HRM_Management.Infrastructure.Services
{
    public class QuartzSchedulerService : ISchedulerService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IServiceProvider _serviceProvider;
        private IScheduler _scheduler;

        public QuartzSchedulerService(ISchedulerFactory factory, IServiceProvider serviceProvider)
        {
            _schedulerFactory = factory;
            _serviceProvider = serviceProvider;
            InitializeSchedulerAsync();
        }

        public async Task InitializeSchedulerAsync()
        {
            _scheduler = await _schedulerFactory.GetScheduler();
            _scheduler.JobFactory = new MicrosoftDependencyInjectionJobFactory(_serviceProvider, null);
            await _scheduler.Start();
        }

        public Task<bool> UnsubscribeAsync(TriggerKey triggerKey)
        {
            return _scheduler.UnscheduleJob(triggerKey);
        }

        public async Task<TriggerKey> SubscribeAsync(IDictionary<string, object>? parameters, string cronSchedule, JobKey jobKey,
            string groupName = "NotificationGroup")
        {
            var id = Guid.NewGuid();
            var triggerKey = new TriggerKey(id.ToString(), groupName);

            var trigger = TriggerBuilder
                .Create()
                .WithIdentity(triggerKey)
                .UsingJobData(new JobDataMap(parameters ?? new Dictionary<string, object>()))
                .WithCronSchedule(cronSchedule)
                .Build();

            var detail = await _scheduler.GetJobDetail(jobKey);
            await _scheduler.ScheduleJob(detail, new[] { trigger }, true);
            return triggerKey;
        }
    }
}
