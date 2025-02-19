using AutoMapper;
using HRM_TgBot.Core.DTOs;
using HRM_TgBot.Core.Models;

namespace HRM_TgBot.Core.Helpers.MappingProfiles;

public class AppMappingProfiles : Profile
{
    public AppMappingProfiles()
    {
        CreateMap<Child, RelativeDto>()
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.BirthDate,
                opt => opt.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.Gender,
                opt => opt.MapFrom(src => src.Gender));


        CreateMap<Partner, RelativeDto>().ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.BirthDate,
                opt => opt.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.Gender,
                opt => opt.MapFrom(src => src.Gender));


        CreateMap<BotUser, PersonDto>()
            .ForMember(dest => dest.TelegramId,
                opt => opt.MapFrom(src => src.UserTgID))
            .ForMember(dest => dest.TelegramUserName,
                opt => opt.MapFrom(src => src.TgUserName))
            .ForMember(dest => dest.FullNameEn,
                opt => opt.MapFrom(src => src.FullNameEn))
            .ForMember(dest => dest.FullNameUa,
                opt => opt.MapFrom(src => src.FullNameUa))
            .ForMember(dest => dest.Email,
                opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber,
                opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.BirthDate,
                opt => opt.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.Hobbies,
                opt => opt.MapFrom(src => src.Hobbies))
            .ForMember(dest => dest.TechStack,
                opt => opt.MapFrom(src => src.TechStack))
            .ForMember(dest => dest.PreviousWorkPlace,
                opt => opt.MapFrom(src => src.PreviousWorkPlace))
            .ForMember(dest => dest.EnglishLevel,
                opt => opt.MapFrom(src => src.EnglishLevel))
            .ForMember(dest => dest.Gender,
                opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.TShirtSize,
                opt => opt.MapFrom(src => src.TShirtSize))
            .ForMember(dest => dest.Children,
                opt => opt.MapFrom(src => src.Children))
            .ForMember(dest => dest.Partner,
                opt => opt.MapFrom(src => src.Partner));
    }
}