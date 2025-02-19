using HRM_Management.Core.DTOs.Enums;

namespace HRM_Management.Core.DTOs.PersonDtos
{
    public record FormUpdateRequest(
        int Id,
        string? FNameEn,
        string? LNameEn,
        string? MNameEn,
        string? FNameUk,
        string? LNameUk,
        string? MNameUk,
        Gender Gender,
        EnglishLevel EnglishLevel,
        TShirtSize TShirtSize,
        DateTime BirthDate,
        string? Hobbies,
        DateTime ApplicationDate,
        string? TechStack,
        string? PrevWorkPlace,
        string? PhoneNumber,
        string? PersonalEmail,
        int TelegramId,
        string? TelegramUserName,
        List<ChildInForm> Children,
        PartnerInForm? Partner);
}
