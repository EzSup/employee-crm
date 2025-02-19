﻿using HRM_TgBot.Core.Helpers.Enums;

namespace HRM_TgBot.Core.Models;

public class Partner
{
    public string FullName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
}