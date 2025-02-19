using HRM_Management.Core.DTOs.Enums;

namespace HRM_Management.Core.DTOs.PersonDtos
{
    public class FormResponse
    {
        public int Id { get; set; }

        public string? FNameEn { get; set; }
        public string? LNameEn { get; set; }
        public string? MNameEn { get; set; }
        public string? FNameUk { get; set; }
        public string? LNameUk { get; set; }
        public string? MNameUk { get; set; }

        public Gender Gender { get; set; }
        public EnglishLevel EnglishLevel { get; set; }
        public TShirtSize TShirtSize { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Hobbies { get; set; }

        public DateTime ApplicationDate { get; set; }
        public string? TechStack { get; set; }
        public string? PrevWorkPlace { get; set; }
        public string? Photo { get; set; }

        public string? PhoneNumber { get; set; }
        public string? PersonalEmail { get; set; }
        public int TelegramId { get; set; }
        public string? TelegramUserName { get; set; }

        public PartnerInForm? Partner { get; set; }
        public List<ChildInForm>? Children { get; set; }
    }
}
