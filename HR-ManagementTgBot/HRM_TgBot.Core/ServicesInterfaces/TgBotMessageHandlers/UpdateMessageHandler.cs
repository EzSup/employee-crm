using HRM_TgBot.Core.ServicesInterfaces;

namespace HRM_TgBot.Bll.MessageHandlers;

public class UpdateMessageHandler
{
    private static IBlogService _blogService;

    public UpdateMessageHandler(IBlogService blogService)
    {
        _blogService = blogService;
    }
}