using AutoMapper;
using HRM_Management.Core.DTOs.NotificationDtos;
using HRM_Management.Core.Helpers;
using HRM_Management.Core.Helpers.Enums;
using HRM_Management.Core.Services;
using HRM_Management.Dal.Entities;
using HRM_Management.Dal.Repositories.Interfaces;
using HRM_Management.Dal.UnitOfWork;
using Quartz;
using static HRM_Management.Bll.Helpers.SubscriptionValidationHelper;
using NotificationJob = HRM_Management.Dal.Entities.Enums.NotificationJob;

namespace HRM_Management.Bll.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<SubscriptionEntity> _notificationRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ISchedulerService _schedulerService;

        public SubscriptionService(IUnitOfWork uow, ISchedulerService schedulerService,
            IMapper mapper)
        {
            _notificationRepository = uow.GetRepository<SubscriptionEntity>();
            _schedulerService = schedulerService;
            _personRepository = (IPersonRepository)uow.GetRepository<PersonEntity>();
            _mapper = mapper;
        }

        public async Task<List<SubscriptionResponse>> GetNotificationsOfPersonAsync(int personId)
        {
            return _mapper.Map<List<SubscriptionResponse>>(await GetNotificationEntitiesOfPersonAsync(personId))
                   ?? new List<SubscriptionResponse>();
        }

        public async Task UnsubscribeAsync(Core.DTOs.Enums.NotificationJob notificationJob, int personId)
        {
            var target = (await GetNotificationEntitiesOfPersonAsync(personId, (NotificationJob)notificationJob)).FirstOrDefault();
            if (target == null) return;
            var succeed = await _schedulerService.UnsubscribeAsync(target.TriggerKey.ToTriggerKey());
            if (succeed) await _notificationRepository.DeleteAsync(target.Id);
        }

        public async Task UnsubscribeAsync(int notificationId)
        {
            var notification = await _notificationRepository.GetByIdAsync(notificationId);
            await _schedulerService.UnsubscribeAsync(notification.TriggerKey.ToTriggerKey());
            await _notificationRepository.DeleteAsync(notificationId);
        }

        public Dictionary<int, string> GetAllNotificationTypes()
        {
            return Enum.GetValues(typeof(Core.DTOs.Enums.NotificationJob))
                .Cast<Core.DTOs.Enums.NotificationJob>()
                .ToDictionary(t => (int)t, t => t.ToString());
        }

        public async Task<int> SubscribeAsync(Core.DTOs.Enums.NotificationJob job, int personId, string cronExpression,
          NotificationType notificationType, IDictionary<string, object>? parameters)
        {
            validateCronExpression(cronExpression);
            var person = await _personRepository.GetByIdAsync(personId);
            ValidateJobSubscriptionExistence(await GetNotificationEntitiesOfPersonAsync(personId, (NotificationJob)job));

            var notification = new SubscriptionEntity
            {
                PersonId = personId,
                Job = NotificationJob.ChildrenBirthdayNotificationJob,
                CronExpression = cronExpression,
            };

            var jobKey = new JobKey(job.ToString());
            parameters ??= new Dictionary<string, object>();
            parameters.Add(new KeyValuePair<string, object>("notificationType", (int)notificationType));

            ValidateNotificationParameters(notificationType, person);

            var chatId = notificationType switch
            {
                NotificationType.EmailNotification => person.PersonalEmail,
                NotificationType.TGBotNotification => person.TelegramId.ToString(),
                _ => throw new NotSupportedException($"Unsupported notification type: {notificationType}")
            };

            parameters.Add(new KeyValuePair<string, object>("chatId", chatId));
            notification.TriggerKey = (await _schedulerService.SubscribeAsync(
                                           parameters, cronExpression, jobKey)).ToString();
            notification.JobKey = jobKey.ToString();
            return (await _notificationRepository.AddAsync(notification)).Id;
        }

        public async Task<Dictionary<int, string>> GetAvailableToSubscribeAsync(int personId)
        {
            var jobs = (await GetNotificationsOfPersonAsync(personId)).Select(x => x.Job);
            var values = Enum.GetValues(typeof(Core.DTOs.Enums.NotificationJob))
                .Cast<Core.DTOs.Enums.NotificationJob>();

            return values
                .Except(jobs)
                .ToDictionary(t => (int)t, t => t.ToString());
        }

        private async Task<IEnumerable<SubscriptionEntity>> GetNotificationEntitiesOfPersonAsync(int personId, NotificationJob? notificationJob = null)
        {
            var notifications = await _notificationRepository.GetWhereAsync(
                                    not => not.PersonId == personId
                                           && notificationJob != null ?
                                               not.Job == notificationJob : true);
            return notifications;
        }
    }
}