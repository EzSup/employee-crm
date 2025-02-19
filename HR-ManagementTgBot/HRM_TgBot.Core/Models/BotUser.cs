using System.ComponentModel.DataAnnotations;
using HRM_TgBot.Core.Helpers.Enums;
using Microsoft.AspNetCore.Http;

namespace HRM_TgBot.Core.Models;

public class BotUser
{
    [Key] public long UserTgID { get; set; }

    public int PersonID { get; set; } = 0;
    public string? TgUserName { get; set; }
    public string? FullNameEn { get; set; } = string.Empty;
    public string? FullNameUa { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }

    [EmailAddress] public string? Email { get; set; }

    public string? Hobbies { get; set; }
    public string? TechStack { get; set; }
    public string? PreviousWorkPlace { get; set; }
    public string? PhoneNumber { get; set; }
    public bool MaritalStatus { get; set; } = false;
    public int? ChildCount { get; set; }
    public List<Child>? Children { get; set; }
    public Partner? Partner { get; set; }

    public WaitType WaitType { get; set; } = WaitType.WaitForFiling;
    public Gender Gender { get; set; } = Gender.None;
    public EnglishLevel EnglishLevel { get; set; } = EnglishLevel.None;
    public TShirtSize TShirtSize { get; set; } = TShirtSize.None;

    public IFormFile? Photo { get; set; }
    public IFormFile? PassportScan { get; set; }
    public IFormFile? LivingPlace { get; set; }
    public IFormFile? IdCode { get; set; }
    public IFormFile? CV { get; set; }

    public Queue<RequestData> RequestData { get; set; } = new();
    public string? FileId { get; set; } = null;
    public string? filePath { get; set; } = null;
    public bool Verified { get; set; }
}