using AutoMapper;
using HRM_Management.Core.DTOs.AuthDtos;
using HRM_Management.Core.DTOs.BlogDto;
using HRM_Management.Core.DTOs.ChildDtos;
using HRM_Management.Core.DTOs.EmployeeDtos;
using HRM_Management.Core.DTOs.HubDtos;
using HRM_Management.Core.DTOs.NotificationDtos;
using HRM_Management.Core.DTOs.PartnerDtos;
using HRM_Management.Core.DTOs.PersonDtos;
using HRM_Management.Core.Helpers;
using HRM_Management.Core.Helpers.Enums;
using HRM_Management.Dal.Entities;
using static HRM_Management.Core.Helpers.Constants;
using static HRM_Management.Core.Helpers.DtoPropertiesHelper;
namespace HRM_Management.Bll.Helpers
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            #region application

            CreateMap<ChildInApplication, ChildEntity>()
                .ReverseMap();

            CreateMap<ChildEntity, ChildrenBirthdayNotificationResponse>()
                .BeforeMap((src, dest) =>
                {
                    dest.Id = src.Id;
                    dest.Name = src.Name;
                    dest.BirthDate = src.BirthDate;
                    dest.ParentName = src.Name;
                })
                .ReverseMap();

            CreateMap<PartnerInApplication, PartnerEntity>()
                .ReverseMap();

            CreateMap<ApplicationSubmitRequest, PersonTranslateEntity>()
                .BeforeMap((src, dest) =>
                {
                    var fullNameParts = src.FullNameUa.SplitFullName(NameFormat.Ukrainian);
                    dest.FNameUk = fullNameParts.FName;
                    dest.LNameUk = fullNameParts.LName;
                    dest.MNameUk = fullNameParts.MName;
                })
                .ReverseMap();

            CreateMap<ApplicationSubmitRequest, PersonEntity>()
                .BeforeMap((src, dest) =>
                {
                    var fullNameParts = src.FullNameEn.SplitFullName(NameFormat.English);
                    dest.FNameEn = fullNameParts.FName;
                    dest.LNameEn = fullNameParts.LName;
                    dest.MNameEn = fullNameParts.MName;
                })
                .ForMember(dest => dest.Gender,
                           opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.EnglishLevel,
                           opt => opt.MapFrom(src => src.EnglishLevel))
                .ForMember(dest => dest.TShirtSize,
                           opt => opt.MapFrom(src => src.TShirtSize))
                .ForMember(dest => dest.BirthDate,
                           opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.Hobbies,
                           opt => opt.MapFrom(src => src.Hobbies))
                .ForMember(dest => dest.ApplicationDate,
                           opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.TechStack,
                           opt => opt.MapFrom(src => src.TechStack))
                .ForMember(dest => dest.PrevWorkPlace,
                           opt => opt.MapFrom(src => src.PreviousWorkPlace))
                .ForMember(dest => dest.PhoneNumber,
                           opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.PersonalEmail,
                           opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.TelegramId,
                           opt => opt.MapFrom(src => src.TelegramId))
                .ForMember(dest => dest.TelegramUserName,
                           opt => opt.MapFrom(src => src.TelegramUserName))
                .ForMember(dest => dest.Children,
                           opt => opt.MapFrom(src => src.Children))
                .ForMember(dest => dest.Partner,
                           opt => opt.MapFrom(src => src.Partner))
                .ForMember(dest => dest.Translate,
                           opt => opt.MapFrom(src => src))
                .ReverseMap();

            #endregion

            #region auth

            CreateMap<UserResponse, UserEntity>()
                .ForMember(dest => dest.UserName,
                           opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.FullName,
                           opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Id,
                           opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<RegisterRequest, UserEntity>()
                .ForMember(dest => dest.UserName,
                           opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email,
                           opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FullName,
                           opt => opt.MapFrom(src => src.FullName))
                .ReverseMap();

            CreateMap<LoginRequest, UserEntity>()
                .ForMember(dest => dest.UserName,
                           opt => opt.MapFrom(src => src.Username))
                .ReverseMap();

            CreateMap<LoginRequest, RegisterRequest>()
                .ForMember(dest => dest.Email,
                           opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password,
                           opt => opt.MapFrom(src => src.Password))
                .ReverseMap();

            #endregion

            #region blogs

            CreateMap<CreateBlogRequest, BlogEntity>()
                .ForMember(dest => dest.Title,
                           opt => opt.MapFrom(src => src.Title))
                .ReverseMap();

            CreateMap<UpDateBlogRequest, BlogEntity>()
                .ForMember(dest => dest.Id,
                           opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title,
                           opt => opt.MapFrom(src => src.Title))
                .ReverseMap();

            CreateMap<BlogEntity, GetBlogResponse>()
                .ForMember(dest => dest.Id,
                           opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title,
                           opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.ContentLink,
                           opt => opt.MapFrom(src => src.ContentLink));

            #endregion

            #region person

            CreateMap<PersonEntity, FormResponse>()
                .ForMember(dest => dest.FNameUk,
                           opt => opt.MapFrom(src => src.Translate.FNameUk))
                .ForMember(dest => dest.LNameUk,
                           opt => opt.MapFrom(src => src.Translate.LNameUk))
                .ForMember(dest => dest.MNameUk,
                           opt => opt.MapFrom(src => src.Translate.MNameUk))
                .ForMember(dest => dest.Photo,
                           opt => opt.MapFrom(src => src.Photo))
                .ForMember(dest => dest.Children,
                           opt => opt.MapFrom(src => src.Children))
                .ForMember(dest => dest.Partner,
                           opt => opt.MapFrom(src => src.Partner))
                .ReverseMap();

            CreateMap<PartnerInForm, PartnerEntity>()
                .ReverseMap();

            CreateMap<ChildInForm, ChildEntity>()
                .ReverseMap();

            CreateMap<PartnerCreateRequest, PartnerEntity>().ReverseMap();

            CreateMap<FormUpdateRequest, PersonEntity>()
                .ForPath(dest => dest.Translate.FNameUk,
                         opt => opt.MapFrom(src => src.FNameUk))
                .ForPath(dest => dest.Translate.LNameUk,
                         opt => opt.MapFrom(src => src.LNameUk))
                .ForPath(dest => dest.Translate.MNameUk,
                         opt => opt.MapFrom(src => src.MNameUk))
                .ForPath(dest => dest.Translate.PersonId,
                         opt => opt.MapFrom(src => src.Id));

            #endregion

            #region hub

            CreateMap<HubCreateRequest, HubEntity>()
                .AfterMap((src, dest) => DtoPropertiesHelper.HandleLeaderIds(dest))
                .ReverseMap();

            CreateMap<HubUpdateRequest, HubEntity>()
                .AfterMap((src, dest) =>DtoPropertiesHelper.HandleLeaderIds(dest))
                .ForMember(dest => dest.Employees,
                           opt => opt.MapFrom(src => src.MemberIds
                                                  .Select(x => new EmployeeEntity { Id = x })))
                .ReverseMap();

            CreateMap<HubEntity, HubResponse>()
                .BeforeMap((src, dest) =>
                {
                    if (src.Director is { Person: not null })
                        dest.DirectorName =
                            DtoPropertiesHelper.UniteFullName(src.Director.Person.FNameEn,
                                                              src.Director.Person.LNameEn,
                                                              src.Director.Person.MNameEn,
                                                              NameFormat.English);
                    if (src.Leader is { Person: not null })
                        dest.DirectorName =
                            DtoPropertiesHelper.UniteFullName(src.Leader.Person.FNameEn,
                                                              src.Leader.Person.LNameEn,
                                                              src.Leader.Person.MNameEn,
                                                              NameFormat.English);
                    if (src.DeputyLeader is { Person: not null })
                        dest.DirectorName =
                            DtoPropertiesHelper.UniteFullName(src.DeputyLeader.Person.FNameEn,
                                                              src.DeputyLeader.Person.LNameEn,
                                                              src.DeputyLeader.Person.MNameEn,
                                                              NameFormat.English);
                })
                .ReverseMap();

            CreateMap<EmployeeEntity, HubMember>()
                .BeforeMap((src, dest) =>
                {
                    if (src.Person is null)
                        return;
                })
                .ForMember(dest => dest.FullName,
                           opt => opt.MapFrom(src => DtoPropertiesHelper
                                                  .UniteFullName(src.Person.FNameEn,
                                                                 src.Person.LNameEn,
                                                                 src.Person.MNameEn,
                                                                 NameFormat.English)))
                .ForMember(dest => dest.TechLevel,
                           opt => opt.MapFrom(src => src.TechLevel.ToString()))
                .ForMember(dest => dest.TechStack,
                           opt => opt.MapFrom(src => src.Person.TechStack))
                .ReverseMap();

            #endregion

            #region notification

            CreateMap<SubscriptionEntity, SubscriptionResponse>()
                .ReverseMap();

            CreateMap<EmployeeEntity, EmployeeBirthdayCongratulationMessageDto>()
                .BeforeMap((src, dest) =>
                    {
                        var rand = new Random();
                        var promptId = rand.Next(0, CONGRATULATION_MESSAGE_PROMPTS.Length - 1);
                        dest.CongratulationMessagePrompt = CONGRATULATION_MESSAGE_PROMPTS[promptId];
                    }
                )
                .ForMember(dest => dest.FirstName,
                           opt => opt.MapFrom(src => src.Person.FNameEn))
                .ForMember(dest => dest.LastName,
                           opt => opt.MapFrom(src => src.Person.LNameEn))
                .ForMember(dest => dest.MiddleName,
                           opt => opt.MapFrom(src => src.Person.MNameEn))
                .ForMember(dest => dest.EmployeePersonalPhotoLink,
                           opt => opt.MapFrom(src => src.Person.Photo));

            CreateMap<EmployeeEntity, EmployeeWelcomeCongratulationDto>()
                .BeforeMap((src, dest) =>
                    {
                        var rand = new Random();
                        var promptId = rand.Next(0, NEW_EMPLOYEE_WELCOME_MESSAGES.Length - 1);
                        dest.CongratulationMessagePrompt = NEW_EMPLOYEE_WELCOME_MESSAGES[promptId];
                    }
                )
                .ForMember(dest => dest.FirstName,
                    opt => opt.MapFrom(src => src.Person.FNameEn))
                .ForMember(dest => dest.LastName,
                    opt => opt.MapFrom(src => src.Person.LNameEn))
                .ForMember(dest => dest.MiddleName,
                    opt => opt.MapFrom(src => src.Person.MNameEn))
                .ForMember(dest => dest.EmployeePersonalPhotoLink,
                    opt => opt.MapFrom(src => src.Person.Photo))
                .ForMember(dest=>dest.Hobbies,
                    opt=>opt.MapFrom(src=>src.Person.Hobbies))
                .ForMember(dest => dest.TechStack,
                    opt 
                        => opt.MapFrom(src => GetFirstTechStackWord(src.Person.TechStack)));

            #endregion
        }
    }
}