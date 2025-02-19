using HRM_TgBot.Core.Helpers.Enums;
using HRM_TgBot.Core.Models;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using static HRM_TgBot.Core.Helpers.Constants.AppConstants;
using static HRM_TgBot.Core.Helpers.Constants.QueueConstantsUK;

namespace HRM_TgBot.Bll.Services;

public static class FormQueueHandler
{
    private static readonly FileHelper _formFileHelper = new();

    public static void BuildQueue(BotUser user)
    {
        user.RequestData.Enqueue(new RequestData
        {
            Message = ENTER_FULL_NAME_EN,
            TryApply = async (enterMessage, botClient) =>
            {
                var isMatch = FULL_NAME_EN_REGEX_PATTERN.IsMatch(enterMessage.Text);
                return isMatch ? (user.FullNameEn = enterMessage.Text) != null : false;
            }
        });
        user.RequestData.Enqueue(new RequestData
        {
            Message = ENTER_FULL_NAME_UK,
            TryApply = async (message, botClient) =>
            {
                var isMatch = FULL_NAME_UK_REGEX_PATTERN.IsMatch(message.Text);
                return isMatch ? (user.FullNameUa = message.Text) != null : false;
            }
        });
        user.RequestData.Enqueue(new RequestData
        {
            Message = ENTER_BIRTHDATE,
            TryApply = async (message, botClient) =>
            {
                if (DateTime.TryParse(message.Text, out var date))
                {
                    if (!ValidateDate(date, DateTime.Now.AddYears(-MINIMAL_AGE_ADULT)))
                    {
                        await botClient.SendTextMessageAsync(message.From.Id, WRONG_DATE);
                        return false;
                    }

                    var utcDateTime = date.ToUniversalTime();
                    user.DateOfBirth = utcDateTime;
                    return true;
                }

                await botClient.SendTextMessageAsync(user.UserTgID, WRONG_BRTHDATE_PATTERN);
                return false;
            },
            Keyboard = new ReplyKeyboardRemove()
        });

        user.RequestData.Enqueue(new RequestData
        {
            Message = CHOOSE_USER_GENDER,
            TryApply = async (message, botClient) =>
            {
                return message.Text.Equals(Enum.GetName(GenderUk.Чоловік), StringComparison.OrdinalIgnoreCase)
                    ?
                    (user.Gender = Gender.Male) != null
                    :
                    message.Text.Equals(Enum.GetName(GenderUk.Жінка), StringComparison.OrdinalIgnoreCase)
                        ? (user.Gender = Gender.Female) != null
                        : false;
            },
            Keyboard =
                new ReplyKeyboardMarkup(
                    Enum.GetNames(typeof(GenderUk))
                        .Select(btn => new KeyboardButton(btn))
                        .Chunk(2)
                        .ToArray()
                )
                {
                    ResizeKeyboard = true,
                    IsPersistent = false
                }
        });

        user.RequestData.Enqueue(new RequestData
        {
            Message = ENTER_EMAIL,
            TryApply = async (message, botClient) =>
            {
                var isMatch = EMAIL_REGEX_PATTERN.IsMatch(message.Text);

                return isMatch ? (user.Email = message.Text) != null : false;
            },
            Keyboard = new ReplyKeyboardRemove()
        });

        user.RequestData.Enqueue(new RequestData
        {
            Message = ENTER_HOBBY,
            TryApply = async (message, botClient) =>
            {
                var isMatch =
                    !string.IsNullOrEmpty(message.Text) && message.Text.Length > MIN_HOBBIES_OR_TECH_STACK_LENGTH
                                                        && message.Text.Length <= MAX_STRING_LENGTH;

                return isMatch ? (user.Hobbies = message.Text) != null : false;
            }
        });

        user.RequestData.Enqueue(new RequestData
        {
            Message = ENTER_TECHSTACK,
            TryApply = async (message, botClient) =>
            {
                var notExceedCapacity =
                    !string.IsNullOrEmpty(message.Text) && message.Text.Length > MIN_HOBBIES_OR_TECH_STACK_LENGTH
                                                        && message.Text.Length <= MAX_STRING_LENGTH;
                return notExceedCapacity ? (user.TechStack = message.Text) != null : false;
            }
        });

        user.RequestData.Enqueue(new RequestData
        {
            Message = ENTER_EXPERIENCE,
            TryApply = async (enterMessage, botClient) =>
            {
                return enterMessage.Text.Equals(Enum.GetName(YesNoEnum.Так), StringComparison.OrdinalIgnoreCase) ? true :
                    enterMessage.Text.Equals(Enum.GetName(YesNoEnum.Ні), StringComparison.OrdinalIgnoreCase) ?
                        user.RequestData.Dequeue() != null : false;
            },
            Keyboard =
                new ReplyKeyboardMarkup(
                    Enum.GetNames(typeof(YesNoEnum))
                        .Select(btn => new KeyboardButton(btn))
                        .Chunk(2)
                        .ToArray()
                )
                {
                    ResizeKeyboard = true,
                    IsPersistent = false
                }
        });

        user.RequestData.Enqueue(new RequestData
        {
            Message = ENTER_PREVIOUSWORKPALE,
            TryApply = async (message, botClient) =>
            {
                var notExceedCapacity = !string.IsNullOrEmpty(message.Text) && message.Text.Length <= MAX_STRING_LENGTH;

                return notExceedCapacity ? (user.PreviousWorkPlace = message.Text) != null : false;
            },
            Keyboard = new ReplyKeyboardRemove()
        });

        user.RequestData.Enqueue(new RequestData
        {
            Message = ENTER_PHONENUMBER,
            TryApply = async (message, botClient) =>
            {
                var isMatch = PHONE_NUMBER_REGEX_PATTERN.IsMatch(message.Text);

                return isMatch ? (user.PhoneNumber = message.Text) != null : false;
            },
            Keyboard = new ReplyKeyboardRemove()
        });

        user.RequestData.Enqueue(new RequestData
        {
            Message = CHOOSE_TSHIRTSIZE,
            TryApply = async (message, botClient) =>
            {
                return Enum.TryParse<TShirtSize>(message.Text, true, out var size) && size != TShirtSize.None
                    ? (user.TShirtSize = size) != null
                    : false;
            },
            Keyboard =
                new ReplyKeyboardMarkup(
                    Enum.GetNames(typeof(TShirtSize))
                        .Where(size => !size.Equals(Enum.GetName(TShirtSize.None)))
                        .Select(size => new KeyboardButton(size))
                        .Chunk(3)
                )
                {
                    ResizeKeyboard = true,
                    IsPersistent = false
                }
        });

        user.RequestData.Enqueue(new RequestData
        {
            Message = CHOOSE_ENGLISH_LEVEL,
            TryApply = async (message, botClient) =>
            {
                return Enum.TryParse<EnglishLevel>(message.Text, true, out var level) && level != EnglishLevel.None
                    ? (user.EnglishLevel = level) != null
                    : false;
            },
            Keyboard =
                new ReplyKeyboardMarkup(
                    Enum.GetNames(typeof(EnglishLevel))
                        .Where(size => !size.Equals(Enum.GetName(EnglishLevel.None)))
                        .Select(size => new KeyboardButton(size))
                        .Chunk(3)
                )
                {
                    ResizeKeyboard = true,
                    IsPersistent = false
                }
        });

        user.RequestData.Enqueue(new RequestData
        {
            Message = SEND_PHOTO_MESSAGE,
            TryApply = async (message, botClient) =>
            {
                var allowedFormats = new[]
                {
                    ".jpg", ".jpeg", ".png"
                };

                if (message.Photo?.Any() == true)
                {
                    var largestPhoto = message.Photo.Last();
                    user.FileId = largestPhoto.FileId;
                }
                else if (message.Document != null)
                {
                    user.FileId = message.Document.FileId;
                }

                if (user.FileId == null)
                    await botClient.SendTextMessageAsync(message.Chat.Id, WRONG_FILE_FORMAT_MESSAGE);

                var file = await botClient.GetFileAsync(user.FileId);

                if (allowedFormats.Contains(Path.GetExtension(file.FilePath)) && file != null)
                {
                    user.Photo = await _formFileHelper.GetFileFromTgAsync(botClient, file.FilePath);
                    return true;
                }

                await botClient.SendTextMessageAsync(user.UserTgID, WRONG_FILE_FORMAT_MESSAGE);
                return false;
            },
            Keyboard = new ReplyKeyboardRemove()
        });

        user.RequestData.Enqueue(new RequestData
        {
            Message = SEND_PASSPORT_SCAN_MESSAGE,
            TryApply = async (message, botClient) =>
            {
                var allowedFormats = new[]
                {
                    ".jpg", ".jpeg", ".png", ".pdf"
                };
                if (message.Photo?.Any() == true)
                {
                    var largestPhoto = message.Photo.Last();
                    user.FileId = largestPhoto.FileId;
                }
                else if (message.Document != null)
                {
                    user.FileId = message.Document.FileId;
                }

                if (user.FileId == null)
                    await botClient.SendTextMessageAsync(message.From.Id, WRONG_FILE_FORMAT_MESSAGE);

                var file = await botClient.GetFileAsync(user.FileId);

                if (allowedFormats.Contains(Path.GetExtension(file?.FilePath)))
                {
                    user.PassportScan = await _formFileHelper.GetFileFromTgAsync(botClient, file.FilePath);
                    return true;
                }

                return false;
            },
            Keyboard = new ReplyKeyboardRemove()
        });

        user.RequestData.Enqueue(new RequestData
        {
            Message = SEND_LIVINGPLASE_MESSAGE,
            TryApply = async (message, botClient) =>
            {
                user.FileId = message.Photo?.Any() == true
                    ? message.Photo.Last().FileId
                    : message.Document?.FileId;

                var allowedFormats = new[]
                {
                    ".jpg", ".jpeg", ".png", ".pdf"
                };

                if (user.FileId == null)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, WRONG_FILE_FORMAT_MESSAGE);
                    return false;
                }

                var file = await botClient.GetFileAsync(user.FileId);
                if (file != null && allowedFormats.Contains(Path.GetExtension(file.FilePath)))
                {
                    user.LivingPlace = await _formFileHelper.GetFileFromTgAsync(botClient, file.FilePath);
                    return true;
                }

                await botClient.SendTextMessageAsync(user.UserTgID, WRONG_FILE_EXTENTIONUA);
                return false;
            }
        });

        user.RequestData.Enqueue(new RequestData
        {
            Message = SEND_IDCODE_MESSAGE,
            TryApply = async (message, botClient) =>
            {
                var allowedFormats = new[]
                {
                    ".jpg", ".jpeg", ".png", ".pdf"
                };
                user.FileId = message.Photo?.Any() == true
                    ? message.Photo.Last().FileId
                    : message.Document?.FileId;

                if (user.FileId == null)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, WRONG_FILE_FORMAT_MESSAGE);
                    return false;
                }

                var file = await botClient.GetFileAsync(user.FileId);

                if (allowedFormats.Contains(Path.GetExtension(file.FilePath)))
                {
                    user.IdCode = await _formFileHelper.GetFileFromTgAsync(botClient, file.FilePath);
                    return true;
                }

                await botClient.SendTextMessageAsync(user.UserTgID, WRONG_FILE_EXTENTIONUA);
                return false;
            },
            Keyboard = new ReplyKeyboardRemove()
        });

        user.RequestData.Enqueue(new RequestData
        {
            Message = SEND_CV_MESSAGE,
            TryApply = async (message, botClient) =>
            {
                var allowedFormats = new[]
                {
                    ".pdf"
                };

                user.FileId = message.Photo?.Any() == true
                    ? message.Photo.Last().FileId
                    : message.Document?.FileId;

                if (user.FileId == null)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, WRONG_FILE_FORMAT_MESSAGE);
                    return false;
                }

                var file = await botClient.GetFileAsync(user.FileId);

                if (allowedFormats.Contains(Path.GetExtension(file?.FilePath)))
                {
                    user.CV = await _formFileHelper.GetFileFromTgAsync(botClient, file.FilePath);
                    return true;
                }

                await botClient.SendTextMessageAsync(user.UserTgID, WRONG_FILE_EXTENTIONUA);
                return false;
            },
            Keyboard = new ReplyKeyboardRemove()
        });

        user.RequestData.Enqueue(new RequestData
        {
            Message = CHOOSE_MARITAL_STATUS,
            TryApply = async (message, botClient) =>
            {
                if (message.Text.Equals(Enum.GetName(YesNoEnum.Так), StringComparison.OrdinalIgnoreCase))
                {
                    user.MaritalStatus = true;
                    return true;
                }

                if (message.Text.Equals(Enum.GetName(YesNoEnum.Ні), StringComparison.OrdinalIgnoreCase))
                {
                    user.MaritalStatus = false;
                    user.RequestData.Dequeue();
                    user.RequestData.Dequeue();
                    user.RequestData.Dequeue();
                    return true;
                }

                return false;
            },
            Keyboard =
                new ReplyKeyboardMarkup(
                    Enum.GetNames(typeof(YesNoEnum))
                        .Select(btn => new KeyboardButton(btn))
                        .Chunk(2)
                        .ToArray()
                )
                {
                    ResizeKeyboard = true,
                    IsPersistent = false
                }
        });

        user.RequestData.Enqueue(new RequestData
        {
            Message = ENTER_PARTNER_NAME_UK,
            TryApply = async (message, botClient) =>
            {
                var isMatch = FULL_NAME_UK_REGEX_PATTERN.IsMatch(message.Text);

                return isMatch
                    ? (user.Partner = new Partner
                    {
                        FullName = message.Text
                    }) != null
                    : false;
            },
            Keyboard = new ReplyKeyboardRemove()
        });

        user.RequestData.Enqueue(new RequestData
        {
            Message = ENTER_PARTNER_BIRTHDATE,
            TryApply = async (message, botClient) =>
            {
                if (DateTime.TryParse(message.Text, out var date))
                {
                    if (!ValidateDate(date, DateTime.Now.AddYears(-MINIMAL_AGE_ADULT)))
                    {
                        await botClient.SendTextMessageAsync(message.From.Id, WRONG_DATE);
                        return false;
                    }

                    var utcDateTime = date.ToUniversalTime();
                    user.Partner.DateOfBirth = utcDateTime;
                    return true;
                }

                await botClient.SendTextMessageAsync(user.UserTgID, WRONG_BRTHDATE_PATTERN);
                return false;
            },
            Keyboard = new ReplyKeyboardRemove()
        });

        user.RequestData.Enqueue(new RequestData
        {
            Message = CHOOSE_GENDER,
            TryApply = async (message, client) =>
            {
                user.Partner.Gender =
                    message.Text.Equals(Enum.GetName(GenderUk.Чоловік), StringComparison.OrdinalIgnoreCase)
                        ? Gender.Male
                        : message.Text.Equals(Enum.GetName(GenderUk.Жінка), StringComparison.OrdinalIgnoreCase)
                            ? Gender.Female
                            : user.Partner.Gender;
                return user.Partner.Gender == Gender.Male || user.Partner.Gender == Gender.Female ? true : false;
            },
            Keyboard =
                new ReplyKeyboardMarkup(
                    Enum.GetNames(typeof(GenderUk))
                        .Select(btn => new KeyboardButton(btn))
                        .Chunk(2)
                        .ToArray()
                )
                {
                    ResizeKeyboard = true,
                    IsPersistent = false
                }
        });

        user.RequestData.Enqueue(new RequestData
        {
            Message = CHOOSE_CHILDREN_EXISTANCE,
            TryApply = async (message, botClient) =>
            {
                if (message.Text.Equals(Enum.GetName(YesNoEnum.Так), StringComparison.OrdinalIgnoreCase))
                {
                    user.RequestData.Enqueue(new RequestData
                    {
                        Message = ENTER_CHILDEN_COUNT,
                        TryApply = async (message, botClient) =>
                        {
                            if (int.TryParse(message.Text, out var count) && count > 0)
                            {
                                user.ChildCount = count;
                                user.Children = new List<Child>(count);

                                for (var i = 0; i < count; i++)
                                {
                                    user.RequestData.Enqueue(new RequestData
                                    {
                                        Message = ENTER_CHILDREN_FULLNAME,
                                        TryApply = async (message, client) =>
                                        {
                                            var isMatch = FULL_NAME_UK_REGEX_PATTERN.IsMatch(message.Text);

                                            if (isMatch)
                                            {
                                                user.Children.Add(new Child
                                                {
                                                    FullName = message.Text
                                                });
                                                return true;
                                            }

                                            await client.SendTextMessageAsync(message.Chat.Id, WRONG_FULLNMAE_PATTERN);
                                            return false;
                                        },
                                        Keyboard = new ReplyKeyboardRemove()
                                    });

                                    user.RequestData.Enqueue(new RequestData
                                    {
                                        Message = ENTER_CHILDREN_BIRTHDATE,
                                        TryApply = async (message, client) =>
                                        {
                                            if (DateTime.TryParse(message.Text, out var date))
                                            {
                                                if (!ValidateDate(date,
                                                        lowerLimitDate: ((DateTime)user.DateOfBirth!).AddYears(
                                                            MINIMAL_AGE_ADULT)))
                                                {
                                                    await botClient.SendTextMessageAsync(message.From.Id, WRONG_DATE);
                                                    return false;
                                                }

                                                var utcDateTime = date.ToUniversalTime();
                                                user.Children.Last().DateOfBirth = utcDateTime;
                                                return true;
                                            }

                                            await botClient.SendTextMessageAsync(user.UserTgID, WRONG_BRTHDATE_PATTERN);
                                            return false;
                                        }
                                    });
                                    user.RequestData.Enqueue(new RequestData
                                    {
                                        Message = CHOOSE_CHILDREN_GENDER,
                                        TryApply = async (message, client) =>
                                        {
                                            var child = user.Children.Last();
                                            if (message.Text.Equals(
                                                    Enum.GetName(typeof(ChildrenGenderUk), ChildrenGenderUk.Син),
                                                    StringComparison.OrdinalIgnoreCase))
                                            {
                                                child.Gender = Gender.Male;
                                            }
                                            else if (message.Text.Equals(
                                                         Enum.GetName(typeof(ChildrenGenderUk),
                                                             ChildrenGenderUk.Донька),
                                                         StringComparison.OrdinalIgnoreCase))
                                            {
                                                child.Gender = Gender.Female;
                                            }
                                            else
                                            {
                                                await client.SendTextMessageAsync(message.From.Id,
                                                    WRONG_CHOOSEN_CHILDREN_GENDER);
                                                return false;
                                            }

                                            return true;
                                        },
                                        Keyboard =
                                            new ReplyKeyboardMarkup(new[]
                                            {
                                                Enum.GetNames(typeof(ChildrenGenderUk))
                                                    .Select(button => new KeyboardButton(button)).ToArray()
                                            })
                                            {
                                                ResizeKeyboard = true,
                                                IsPersistent = false
                                            }
                                    });
                                }

                                return true;
                            }

                            await botClient.SendTextMessageAsync(message.Chat.Id, WRONG_ENTERED_CHILDREN_COUNT);
                            return false;
                        },
                        Keyboard = new ReplyKeyboardRemove()
                    });
                    return true;
                }

                return true;
            },
            Keyboard =
                new ReplyKeyboardMarkup(
                    Enum.GetNames(typeof(YesNoEnum))
                        .Select(btn => new KeyboardButton(btn))
                        .Chunk(2)
                        .ToArray()
                )
                {
                    ResizeKeyboard = true,
                    IsPersistent = true
                }
        });
    }

    private static bool ValidateDate(DateTime dateToValidate, DateTime? upperLimitDate = null,
        DateTime? lowerLimitDate = null)
    {
        upperLimitDate = upperLimitDate ?? DateTime.Now;
        lowerLimitDate = lowerLimitDate ?? new DateTime(1900, 1, 1);

        return dateToValidate < upperLimitDate && dateToValidate > lowerLimitDate;
    }
}