using HRM_Management.Core.Helpers.Enums;
namespace HRM_Management.Core.Helpers
{
    public static class Constants
    {
        public const string MAIL_CHILDREN_NOTIFICATION_SUBJECT_PROMPT = "Upcoming Birthdays of Employees' Children Next Week!";
        public const string MAIL_EMPLOYEE_BIRTHDAY_CONGRATULATION_SUBJECT_PROMPT = "Happy Birthday to {0}!";
        public const string MAIL_EMPLOYEE_WELCOME_SUBJECT_PROMP = "Welcome on board {0}!";
        public const string MAIL_EMPLOYEE_MESSAGE_SUBJECT = "For   Employees!";

        /// <summary>
        ///     If you need to add new template you need to change Copy to out directory property to be "Copy always" in the file
        ///     properties
        /// </summary>
        public const string MAIL_CHILDREN_NOTIFICATION_TEMPLATE = "BirthdayTemplate.cshtml";
        public const string MAIL_EMPLOYEE_BIRTHDAY_CONGRATULATION_TEMPLATE = "EmployeeBirthdayCongratulationTemplate.cshtml";
        public const string MAIL_EMPLOYEE_WELCOME_CONGRATULATION_TEMPLATE = "EmployeeWelcomeCongratulationTemplate.cshtml";
        public const string MAIL_MESSAGE_TEMPLATE = "MessageTemplate.cshtml";
        public const string MAIL_TEMPLATES_PATH = @"Infrastructure\MailFileTemplates";

        public const double MAX_FILE_SIZE_BYTES = 15 * (1024.0 * 1024.0);
        public const string EN_NAME_REGEX_PATTERN = @"\b[A-Z][a-z]*\b";
        public const string UA_NAME_REGEX_PATTERN = @"\b[А-ЯҐЄІЇ][а-яґєії]*\b";
        public const string FULL_NAME_EN_REGEX_PATTERN = @"\b[A-Z][a-z-']*\b\s\b[A-Z][a-z-']*\b\s\b[A-Z][a-z-']*\b";
        public const string FULL_NAME_UA_REGEX_PATTERN = @"\b[А-ЯҐЄІЇ][а-яґєії'-]*\b\s\b[А-ЯҐЄІЇ][а-яґєії'-]*\b\s\b[А-ЯҐЄІЇ][а-яґєії'-]*\b";
        public const string PHONE_NUM_REGEX_PATTERN = @"\+?\d+";
        public const string CORPORATIVE_EMAIL_REGEX_PATTERN = @"^\w+([-+.']\w+)*@ sysdev.com$";
        public const string USERNAME_OR_PASSWORD_NOT_PROVIDED_MESSAGE = "Username and/or password was not provided";

        public const string TgBotHttpClientName = "TgBlogClient";

        public const int JOBKEY_PARTS_COUNT = 2;

        public const string NO_DATA_TABLE_VALUE = "no data";

        public static readonly TimeSpan DEFAULT_EMAIL_IMAGES_TTL = TimeSpan.FromDays(1);

        public static readonly Dictionary<DocumentType, string[]> ACCEPTABLE_FILE_EXTENSIONS = new Dictionary<DocumentType, string[]>
        {
            { DocumentType.Photo, new[] { ".png", ".jpg", ".jpeg" } },
            { DocumentType.CV, new[] { ".pdf", ".docx" } },
            { DocumentType.PassportScan, new[] { ".png", ".jpg", ".jpeg", ".pdf" } }
        };

        public static readonly DateTime MIN_VALID_DATE = new DateTime(1900, 1, 1);
        public static readonly string[] LEADERS_IDS_COLUMN_NAMES = { "DirectorId", "LeaderId", "DeputyLeaderId" };
        public static readonly Dictionary<string, string> FIELD_NAMES_INTERPRETATIONS = new Dictionary<string, string>
        {
            { "id", "ID" },
            { "name", "Name" },
            { "birthDate", "Birth Date" },
            { "email", "Email" },
            { "gender", "Gender" },
            { "techLevel", "Tech Level" },
            { "dateOfComing", "Date Of Coming" },
            { "applicationData", "Application Date" },
            { "mentorName", "Mentor Name" },
            { "fNameEn", "First Name" },
            { "lNameEn", "Last Name" },
            { "mNameEn", "Middle Name" },
            { "fNameUk", "First Name(Ukr)" },
            { "lNameUK", "Last Name(Ukr)" },
            { "mNameUk", "Middle Name(Ukr)" },
            { "hobbies", "Hobbies" },
            { " Mail", "  Email" },
            { "telegramId", "Telegram Id" },
            { "telegramUserName", "Telegram UserName" },
            { "leaderName", "Leader Name" },
            { "deputyLeaderName", "Deputy Leader Name" },
            { "directorName", "Director Name" },
            { "personalEmail", "Personal Email" },
            { "englishLevel", "English Level" },
            { "phoneNumber", "Phone Number" },
            { "photo", "Photo" }
        };

        public static readonly string[] CONGRATULATION_MESSAGE_PROMPTS =
        {
            "May you have a great day today and the year ahead is full of many blessings.\nWishing you get success in all the tasks you start.\nEnjoy a cheerful birthday!",
            "Wishing you a day of relaxation and enjoyment.\nMay the year ahead be as rewarding and fulfilling as you envision.",
            "May your day be filled with laughter and your year with incredible achievements.\nWishing you all the best on your special day!"
        };

        public static readonly string[] NEW_EMPLOYEE_WELCOME_MESSAGES =
        {
            "Welcome aboard! \nWe’re excited to have you on the team. Wishing you a great start, exciting challenges, and many achievements ahead!",
            "Glad to have you with us! \nA new journey begins, and we can’t wait to see your impact. Wishing you growth, success, and a great experience!",
            "Welcome to the team! \nEvery new start brings new opportunities. We believe in your potential and look forward to achieving great things together!"
        };

    }
}
