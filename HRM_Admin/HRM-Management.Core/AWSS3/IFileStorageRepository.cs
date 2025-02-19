using Microsoft.AspNetCore.Http;
namespace HRM_Management.Core.AWSS3
{
    public interface IFileStorageRepository
    {
        Task<string> UploadFileAsync(IFormFile file, string? folderName = null, string? fileName = null);
        Task<string> GetObjectTempUrlAsync(string key, TimeSpan? ttl = null);
        Task<bool> CheckFileExistenceAsync(string key);
        Task DeleteFileAsync(string key);
    }
}
