using Amazon.S3;
using Amazon.S3.Model;
using HRM_Management.Core.AWSS3;
using HRM_Management.DalS3.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System.Net;
namespace HRM_Management.DalS3
{
    public class FileStorageRepository : IFileStorageRepository
    {
        private readonly S3Options _options;
        private readonly IDistributedCache _redis;
        private readonly IAmazonS3 _s3Client;
        private readonly int _ttlInMinutes;

        public FileStorageRepository(IAmazonS3 s3Client, IOptions<S3Options> options, IDistributedCache cache,
            IOptions<S3Options> s3Options)
        {
            _options = options.Value;
            _s3Client = s3Client;
            _redis = cache;
            _ttlInMinutes = s3Options.Value.TTLInMinutes <= 0 ? 60 : s3Options.Value.TTLInMinutes;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string? folderName, string? fileName)
        {
            var newName = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);
            var filePath = fileName ?? newName + Path.GetExtension(file.FileName);
            var key = string.IsNullOrEmpty(folderName) ? filePath : $"{folderName}/{filePath}";

            var putRequest = new PutObjectRequest
            {
                Key = key,
                BucketName = _options.S3BucketName,
                InputStream = file.OpenReadStream(),
                ContentType = file.ContentType
            };
            await _s3Client.PutObjectAsync(putRequest);

            return key;
        }

        public async Task<bool> CheckFileExistenceAsync(string key)
        {
            var request = new GetObjectMetadataRequest
            {
                Key = key,
                BucketName = _options.S3BucketName
            };
            var response = await _s3Client.GetObjectMetadataAsync(request);

            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        public async Task DeleteFileAsync(string key)
        {
            if (await CheckFileExistenceAsync(key) == false)
            {
                throw new Exception("File what you are attempting  to delete is not exists");
            }
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _options.S3BucketName,
                Key = key
            };
            await _s3Client.DeleteObjectAsync(deleteRequest);
        }

        public async Task<string> GetObjectTempUrlAsync(string key, TimeSpan? ttl = null)
        {
            var cacheKey = ToCacheKey(key);
            var fromCache = await _redis.GetStringAsync(cacheKey);
            if (!string.IsNullOrWhiteSpace(fromCache))
                return fromCache;

            if (await CheckFileExistenceAsync(key) == false)
            {
                throw new Exception("File does not exist");
            }

            ttl ??= TimeSpan.FromMinutes(_ttlInMinutes);

            var preSignedUrlRequest = new GetPreSignedUrlRequest
            {
                Key = key,
                BucketName = _options.S3BucketName,
                Expires = DateTime.UtcNow.Add((TimeSpan)ttl),
                Verb = HttpVerb.GET
            };

            var fileUrl = await _s3Client.GetPreSignedURLAsync(preSignedUrlRequest);
            await SaveStringToCacheAsync(cacheKey, fileUrl, (TimeSpan)ttl);
            return fileUrl;
        }

        private async Task SaveStringToCacheAsync(string key, string value, TimeSpan ttl)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = ttl
            };
            await _redis.SetStringAsync(key, value, options);
        }

        private string ToCacheKey(string key)
        {
            return $"file-{key}";
        }
    }
}
