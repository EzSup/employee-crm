using HRM_Management.Dal.Entities.Enums;

namespace HRM_Management.Dal.Entities
{
    public class PersonEntity
    {
        public int Id { get; set; }

        public required string FNameEn { get; set; }
        public required string LNameEn { get; set; }
        public required string MNameEn { get; set; }

        public Gender Gender { get; set; }
        public EnglishLevel EnglishLevel { get; set; }
        public TShirtSize TShirtSize { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Hobbies { get; set; }

        public DateTime ApplicationDate { get; set; }
        public string? TechStack { get; set; }
        public string? PrevWorkPlace { get; set; }
        public string? CV { get; set; }
        public string? Photo { get; set; }
        public string? PassportScan { get; set; }

        public string? PhoneNumber { get; set; }
        public string? PersonalEmail { get; set; }
        public int TelegramId { get; set; }
        public string? TelegramUserName { get; set; }

        public int? PartnerId { get; set; }

        public PartnerEntity? Partner { get; set; }
        public List<ChildEntity>? Children { get; set; }
        public EmployeeEntity? Employee { get; set; }
        public PersonTranslateEntity? Translate { get; set; }
        public List<SubscriptionEntity>? Notifications { get; set; }
    }
}
