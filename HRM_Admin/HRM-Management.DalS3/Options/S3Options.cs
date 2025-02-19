using Microsoft.Extensions.Configuration;

namespace HRM_Management.DalS3.Options
{
    public class S3Options
    {
        [ConfigurationKeyName("AccessKey")] public string AccessKey { get; set; }
        [ConfigurationKeyName("SecretKey")] public string SecretKey { get; set; }
        [ConfigurationKeyName("Region")] public string Region { get; set; }
        [ConfigurationKeyName("S3Bucket")] public string S3BucketName { get; set; }
        [ConfigurationKeyName("S3BlogFolder")] public string S3BlogFolder { get; set; }
        [ConfigurationKeyName("S3FileLinkTTLInMinutes")] public int TTLInMinutes { get; set; }
    }
}
