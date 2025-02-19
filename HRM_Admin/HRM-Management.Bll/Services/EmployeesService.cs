using AutoMapper;
using HRM_Management.Core.AWSS3;
using HRM_Management.Core.DTOs.EmployeeDtos;
using HRM_Management.Core.DTOs.HubDtos;
using HRM_Management.Core.Helpers;
using HRM_Management.Core.Helpers.Enums;
using HRM_Management.Core.Services;
using HRM_Management.Dal.Entities;
using HRM_Management.Dal.Repositories.Interfaces;
using HRM_Management.Dal.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
namespace HRM_Management.Bll.Services
{
    public class EmployeesService :
        IEmployeesService
    {
        private readonly IRepository<EmployeeEntity> _employeesRepository;
        private readonly IFileStorageRepository _fileStorageRepository;
        private readonly IMapper _mapper;
        private readonly IMessageServiceFactory _messageServiceFactory;
        private readonly INotificationMessageProviderFactory _notificationMessageProviderFactory;


        public EmployeesService(IUnitOfWork unitOfWork, IFileStorageRepository fileStorage, IMapper mapper, IMessageServiceFactory messageServiceFactory,
            INotificationMessageProviderFactory notificationMessageProviderFactory)
        {
            _employeesRepository = (IEmployeeRepository)unitOfWork.GetRepository<EmployeeEntity>();
            _fileStorageRepository = fileStorage;
            _mapper = mapper;
            _messageServiceFactory = messageServiceFactory;
            _notificationMessageProviderFactory = notificationMessageProviderFactory;
        }

        public Task<int> GetTotalCountAsync()
        {
            return _employeesRepository
                   .GetAllQueryable()
                   .CountAsync();
        }

        public async Task HandleEmployeesDocumentsAsync(int employeeId, params IFormFile[] docs)
        {
            var nonNullDocs = docs.Where(doc => doc != null).ToList();
            if (!nonNullDocs.Any())
                return;
            var employee = await _employeesRepository.GetByIdAsync(employeeId);
            employee.Documents ??= new List<string>();

            foreach (var doc in nonNullDocs)
            {
                if (doc != null)
                    employee!.Documents.Add(await _fileStorageRepository.UploadFileAsync(doc));
            }

            await _employeesRepository.UpdateAsync(employee);
        }

        public async Task<IEnumerable<HubMember>> GetNotAssignedToHubEmployeesAsync()
        {
            return _mapper.Map<IEnumerable<HubMember>>(
                await _employeesRepository.GetAllQueryable()
                                          .Include(emp => emp.Person)
                                          .Where(x => x.HubId == null)
                                          .ToListAsync());
        }

        public async Task<IEnumerable<string>> GetAllEmployeesEmailsAsync()
        {
            return await _employeesRepository.GetAllQueryable()
                                             .Select(x => x. Mail)
                                             .Where(x => !string.IsNullOrWhiteSpace(x))
                                             .ToListAsync();
        }

        public async Task EmployeeBirthdayCongratulateAsync(string[] chats, NotificationType notificationType)
        {
            var messageService = _messageServiceFactory.CreateMessageService(notificationType);
            var messageProvider = _notificationMessageProviderFactory.CreateMessageProvider(notificationType);

            var employees = await GetEmployeesWithBirthdayTodayAsync();

            if (!employees.Any())
                return;

            var message = await messageProvider.GenerateEmployeeBirthdayCongratulationMessageAsync(employees);
            message.Subject = string.Format(Constants.MAIL_EMPLOYEE_BIRTHDAY_CONGRATULATION_SUBJECT_PROMPT,
                                            DtoPropertiesHelper.WriteNamesInAList(employees.Select(x => x.FirstName).ToArray()));
            await messageService.SendMessageAsync( chats,message);
        }

        public async Task SendWelcomeNotificationAsync (string [] chats,NotificationType notificationType)
        {
            var messageService = _messageServiceFactory.CreateMessageService(NotificationType.EmailNotification); 
            var messageProvider = _notificationMessageProviderFactory.CreateMessageProvider(NotificationType.EmailNotification);
            var employees = await GetNewEmployeesWhatCameYesterday();
            
            if (!employees.Any())
                return;
            foreach (var employeeWelcome in employees)
            {
                var message = await messageProvider.GenerateEmployeeWelcomeMessageAsync(employeeWelcome);
                message.Subject = string.Format(Constants.MAIL_EMPLOYEE_WELCOME_SUBJECT_PROMP,
                    DtoPropertiesHelper.WriteNamesInAList(employees.Select(x => x.FirstName).ToArray()));
                await messageService.SendMessageAsync(chats, message);
            }
        } 

        public async Task<IEnumerable<EmployeeBirthdayCongratulationMessageDto>> GetEmployeesWithBirthdayTodayAsync()
        {
            var employees = await _employeesRepository.GetAllQueryable()
                                                      .Include(x => x.Person)
                                                      .Where(x => x.Person.BirthDate.DayOfYear == DateTime.Today.DayOfYear)
                                                      .ToListAsync();
            var notNullPhotosEmployees = employees.Where(x => x.Person != null && !string.IsNullOrWhiteSpace(x.Person.Photo));
            foreach (var emp in notNullPhotosEmployees)
                emp.Person.Photo = await _fileStorageRepository.GetObjectTempUrlAsync(emp.Person.Photo, Constants.DEFAULT_EMAIL_IMAGES_TTL);

            return _mapper.Map<IEnumerable<EmployeeBirthdayCongratulationMessageDto>>(employees);
        }

        public async Task<IEnumerable<EmployeeWelcomeCongratulationDto>> GetNewEmployeesWhatCameYesterday()
        {
            var yesterday = DateTime.UtcNow.AddDays(-1);
            var today = DateTime.UtcNow;
            var employees = await _employeesRepository.GetAllQueryable()
                                                      .Include(e=>e.Person)
                                                      .Where(e => e.DateOfComing >= yesterday && e.DateOfComing <= today)
                                                      .ToListAsync();
            var notNullPhotosEmployees = employees.Where(x => x.Person != null && !string.IsNullOrWhiteSpace(x.Person.Photo));
            foreach (var emp in notNullPhotosEmployees)
                emp.Person.Photo = await _fileStorageRepository.GetObjectTempUrlAsync(emp.Person.Photo);
            
            return _mapper.Map<IEnumerable<EmployeeWelcomeCongratulationDto>>(employees);
        }
    }
}