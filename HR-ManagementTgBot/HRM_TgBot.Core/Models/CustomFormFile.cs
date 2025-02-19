using Microsoft.AspNetCore.Http;

namespace HRM_TgBot.Core.Models;

public class CustomFormFile : IFormFile
{
    private readonly byte[] _fileBytes;

    public CustomFormFile(byte[] fileBytes, string fileName, string contentType)
    {
        _fileBytes = fileBytes;
        FileName = fileName;
        ContentType = contentType;
    }

    public string ContentType { get; }
    public string FileName { get; }
    public long Length => _fileBytes.Length;
    public string Name => "file";

    public string ContentDisposition => throw new NotImplementedException();
    public IHeaderDictionary Headers => throw new NotImplementedException();

    public void CopyTo(Stream target)
    {
        target.Write(_fileBytes, 0, _fileBytes.Length);
    }

    public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
    {
        return target.WriteAsync(_fileBytes, 0, _fileBytes.Length, cancellationToken);
    }

    public Stream OpenReadStream()
    {
        return new MemoryStream(_fileBytes);
    }
}