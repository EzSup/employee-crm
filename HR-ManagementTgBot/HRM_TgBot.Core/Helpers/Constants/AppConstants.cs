using System.Text.RegularExpressions;

namespace HRM_TgBot.Core.Helpers.Constants;

public static class AppConstants
{
    public const string BLOG_HTTP_CLIENT_NAME = "BlogHttpClient";

    public const int MIN_HOBBIES_OR_TECH_STACK_LENGTH = 4;

    public const int MAX_STRING_LENGTH = 100;

    public const int MINIMAL_AGE_ADULT = 16;

    public static readonly Regex FULL_NAME_UK_REGEX_PATTERN =
        new(@"\b[А-ЯҐЄІЇ][а-яґєії'-]*\b\s\b[А-ЯҐЄІЇ][а-яґєії'-]*\b\s\b[А-ЯҐЄІЇ][а-яґєії'-]*\b");

    public static readonly Regex FULL_NAME_EN_REGEX_PATTERN =
        new(@"\b[A-Z][a-z'-]*\b\s\b[A-Z][a-z'-]*\b\s\b[A-Z][a-z'-]*\b");

    public static readonly Regex EMAIL_REGEX_PATTERN = new(@"[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9]+");

    public static readonly Regex PHONE_NUMBER_REGEX_PATTERN = new(@"380\d{9}$");
}