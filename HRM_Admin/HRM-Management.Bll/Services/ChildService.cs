using AutoMapper;
using HRM_Management.Core.DTOs.ChildDtos;
using HRM_Management.Core.Helpers.Enums;
using HRM_Management.Core.Services;
using HRM_Management.Dal.Entities;
using HRM_Management.Dal.UnitOfWork;
using Microsoft.EntityFrameworkCore;
namespace HRM_Management.Bll.Services
{
    public class ChildService : IChildService
    {
        private readonly IRepository<ChildEntity> _childRepository;
        private readonly IMailTemplateFileGenerationHelper _emailFileGenerator;
        private readonly IMapper _mapper;
        private readonly IMessageServiceFactory _messageServiceFactory;
        private readonly INotificationMessageProviderFactory _notificationMessageProviderFactory;


        public ChildService(IUnitOfWork unitOfWork, IMapper mapper, IMessageServiceFactory messageServiceFactory,
            INotificationMessageProviderFactory notificationMessageProviderFactory)
        {
            _mapper = mapper;
            _messageServiceFactory = messageServiceFactory;
            _notificationMessageProviderFactory = notificationMessageProviderFactory;
            _childRepository = unitOfWork.GetRepository<ChildEntity>();
        }

        public async Task DeleteAsync(int id)
        {
            await _childRepository.DeleteAsync(id);
        }

        public async Task NotifyNextWeekChildrenBirthday(string[] chats, NotificationType notificationType)
        {
            var messageService = _messageServiceFactory.CreateMessageService(notificationType);
            var messageProvider = _notificationMessageProviderFactory.CreateMessageProvider(notificationType);

            var message = await messageProvider.GenerateChildrenBirthdayMessageAsync(await GetChildrenWithBirthdayNextWeek());
            await messageService.SendMessageAsync( chats,message);
        }

        private DateTime GetNextWeekFirstDay()
        {
            var today = DateTime.UtcNow.Date;
            var daysUntilNextMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
            var firstDayOfNextWeek = today.AddDays(daysUntilNextMonday);
            return firstDayOfNextWeek;
        }

        private async Task<IEnumerable<ChildrenBirthdayNotificationResponse>> GetChildrenWithBirthdayNextWeek()
        {
            var firstDayOfNextWeek = GetNextWeekFirstDay();
            var minDayOfYear = firstDayOfNextWeek.DayOfYear;
            var maxDayOfYear = firstDayOfNextWeek.AddDays(7).DayOfYear;

            var children = await _childRepository
                                 .GetAllQueryable()
                                 .Include(x => x.Parent)
                                 .ThenInclude(x => x.Translate)
                                 .Where(x =>
                                            minDayOfYear < maxDayOfYear// if week starts and ends in different years we check by second condition
                                                ? x.BirthDate.DayOfYear >= minDayOfYear &&
                                                  x.BirthDate.DayOfYear < maxDayOfYear
                                                : x.BirthDate.DayOfYear >= minDayOfYear ||
                                                  x.BirthDate.DayOfYear < maxDayOfYear)
                                 .ToListAsync();

            return _mapper.Map<IEnumerable<ChildrenBirthdayNotificationResponse>>(children);
        }
    }
}
