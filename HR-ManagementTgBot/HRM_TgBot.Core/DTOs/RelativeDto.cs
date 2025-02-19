using System.Text.Json.Serialization;
using HRM_TgBot.Core.Helpers.Enums;

namespace HRM_TgBot.Core.DTOs;

public record RelativeDto
{
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;

    [JsonPropertyName("birthDate")] public DateTime BirthDate { get; set; }

    [JsonPropertyName("gender")] public Gender Gender { get; set; }
}