using HRM_Management.Core.DTOs.Enums;

namespace HRM_Management.Core.DTOs.PersonDtos
{
    public record ApplicationSubmitRequest(
        int TelegramId,
        string? TelegramUserName,
        string FullNameEn,
        string FullNameUa,
        string Email,
        string PhoneNumber,
        DateTime BirthDate,
        string Hobbies,
        string TechStack,
        string? PreviousWorkPlace,
        EnglishLevel EnglishLevel,
        Gender Gender,
        TShirtSize TShirtSize,
        ChildInApplication[]? Children,
        PartnerInApplication? Partner);
}
