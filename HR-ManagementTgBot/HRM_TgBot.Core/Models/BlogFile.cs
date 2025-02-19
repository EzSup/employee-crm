namespace HRM_TgBot.Core.Models;

public class BlogFile
{
    public BlogFile(string fullFileName, MemoryStream FileStream)
    {
        this.FileStream = FileStream;
        FullFileName = fullFileName;
    }

    public MemoryStream FileStream { get; }
    public string FullFileName { get; }
}