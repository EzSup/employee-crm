using System.Text.Json.Serialization;

namespace HRM_TgBot.Core.DTOs;

public record PersonDto
{
    [JsonPropertyName("telegramId")] public long TelegramId { get; set; }

    [JsonPropertyName("telegramUserName")] public string TelegramUserName { get; set; } = string.Empty;

    [JsonPropertyName("fullNameEn")] public string FullNameEn { get; set; } = string.Empty;

    [JsonPropertyName("fullNameUa")] public string FullNameUa { get; set; } = string.Empty;

    [JsonPropertyName("email")] public string Email { get; set; } = string.Empty;

    [JsonPropertyName("phoneNumber")] public string PhoneNumber { get; set; } = string.Empty;

    [JsonPropertyName("birthDate")] public DateTime BirthDate { get; set; }

    [JsonPropertyName("hobbies")] public string Hobbies { get; set; } = string.Empty;

    [JsonPropertyName("techStack")] public string TechStack { get; set; } = string.Empty;

    [JsonPropertyName("previousWorkPlace")]
    public string PreviousWorkPlace { get; set; } = string.Empty;

    [JsonPropertyName("englishLevel")] public int EnglishLevel { get; set; }

    [JsonPropertyName("gender")] public int Gender { get; set; }

    [JsonPropertyName("tShirtSize")] public int TShirtSize { get; set; }

    [JsonPropertyName("children")] public List<RelativeDto>? Children { get; set; }

    [JsonPropertyName("partner")] public RelativeDto? Partner { get; set; }
}