using AutoMapper;
using HRM_Management.Bll.Helpers;
using HRM_Management.Bll.Services;
using HRM_Management.Core.DTOs.ChildDtos;
using HRM_Management.Core.DTOs.NotificationDtos;
using HRM_Management.Core.Helpers.Enums;
using HRM_Management.Core.Services;
using HRM_Management.Dal.Entities;
using HRM_Management.Dal.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;

using static HRM_Management.Tests.Helpers.StorageConfigurationHelper;

namespace HRM_Management.Tests.Bll;
public class ChildServiceTest
{
    private readonly Mock<DbSet<ChildEntity>> _mockSet;
    private readonly Mock<IUnitOfWork> _UnitOfWork;
    private readonly Mock<IRepository<ChildEntity>> _childRepository;

    private readonly Mock<IMessageService> _messageService = new();
    private readonly Mock<INotificationMessageProvider> _notificationMessageProvider = new();
    private readonly Mock<IMessageServiceFactory> _messageServiceFactory = new();
    private readonly Mock<INotificationMessageProviderFactory> _notificationMessageProviderFactory = new();

    private readonly IMapper _mapper;
    private ChildService _childService;

    public ChildServiceTest()
    {
        _UnitOfWork = new Mock<IUnitOfWork>();
        _childRepository = new Mock<IRepository<ChildEntity>>();

        var mapperProfiles = new AppMappingProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfiles));
        _mapper = new Mapper(configuration);

        ConfigureDbSet(out _mockSet, GetTestChildren());

        _notificationMessageProvider.Setup(x =>
                x.GenerateChildrenBirthdayMessageAsync(It.IsAny<IEnumerable<ChildrenBirthdayNotificationResponse>>()))
            .ReturnsAsync(GetSendingMessage());

        _notificationMessageProviderFactory.Setup(x => x.CreateMessageProvider(It.IsAny<NotificationType>()))
            .Returns(_notificationMessageProvider.Object);

        _childRepository.Setup(repo => repo.GetAllQueryable())
            .Returns(_mockSet.Object);

        _UnitOfWork.Setup(uow => uow.GetRepository<ChildEntity>(true))
                .Returns(_childRepository.Object);
    }

    [Theory]
    [InlineData(NotificationType.EmailNotification)]
    [InlineData(NotificationType.TGBotNotification)]
    public async Task NotifyNextWeekChildrenBirthday_WhenChildrenHaveBirthdaysNextWeek_SendsMessageToEachChat(NotificationType notificationType)
    {
        //arrange
        var chats = new string[] { "chat1", "chat2" };
        var expectedMessage = GetSendingMessage();

        SendingMessageDto actualMessage = null;
        string[] actualChats = null;


        _messageService
            .Setup(x => x.SendMessageAsync(It.IsAny<string[]>(), It.IsAny<SendingMessageDto[]>()))
            .Callback<string[], SendingMessageDto[]>((receivers, messages) =>
            {
                actualMessage = messages[0];
                actualChats = chats;
            })
            .Returns(Task.CompletedTask);
        
        _messageServiceFactory.Setup(x => x.CreateMessageService(Moq.It.IsAny<NotificationType>()))
            .Returns(_messageService.Object);

        _childService = new ChildService(_UnitOfWork.Object, _mapper, _messageServiceFactory.Object, _notificationMessageProviderFactory.Object);
        //Act
        await _childService.NotifyNextWeekChildrenBirthday(chats, notificationType);
        //Assert
        _childRepository.Verify(repo => repo.GetAllQueryable(), Times.Once);
        _notificationMessageProviderFactory.Verify(x => x.CreateMessageProvider(It.IsAny<NotificationType>()), Times.Once);
        _notificationMessageProvider.Verify(x => x.GenerateChildrenBirthdayMessageAsync(It.IsAny<IEnumerable<ChildrenBirthdayNotificationResponse>>()), Times.Once);
        _messageServiceFactory.Verify(x => x.CreateMessageService(Moq.It.IsAny<NotificationType>()), Times.Once);
        _messageService.Verify(x => x.SendMessageAsync(It.IsAny<string[]>(), It.IsAny<SendingMessageDto[]>()), Times.Once);
        Assert.Equal(chats, actualChats);
        Assert.Equal(expectedMessage.MessageCotent, actualMessage.MessageCotent);
        Assert.Equal(expectedMessage.Subject, actualMessage.Subject);
    }

    private SendingMessageDto GetSendingMessage() => new SendingMessageDto("messageTest", "subjectTest");

    private List<ChildEntity> GetTestChildren()
    {
        var today = DateTime.UtcNow.Date;
        var daysUntilNextMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;

        var startOfNextWeekTwoYearsAgo = today.AddDays(daysUntilNextMonday).AddYears(-2);
        var endOfNextWeekTwoYearsAgo = startOfNextWeekTwoYearsAgo.AddDays(6);

        return new List<ChildEntity>
        {
            new ChildEntity
            {
                Id = 1,
                Name = "Іван",
                BirthDate = startOfNextWeekTwoYearsAgo,
                Parent = new PersonEntity { Id = 1, FNameEn = "Oleg", MNameEn = "Olegovich", LNameEn = "Markovich" }
            },
            new ChildEntity
            {
                Id = 2,
                Name = "Марія",
                BirthDate = startOfNextWeekTwoYearsAgo.AddDays(3),
                Parent = new PersonEntity { Id = 2, FNameEn = "Oleg", MNameEn = "Olegovich", LNameEn = "Markovich" }
            },
            new ChildEntity
            {
                Id = 3,
                Name = "Артем",
                BirthDate = endOfNextWeekTwoYearsAgo,
                Parent = new PersonEntity { Id = 3, FNameEn = "Oleg", MNameEn = "Olegovich", LNameEn = "Markovich" }
            }
        };
    }
}