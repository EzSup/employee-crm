using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using HRM_Management.Core.AWSS3;
using HRM_Management.DalS3.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HRM_Management.DalS3.Extensions
{
    public static class S3Extensions
    {
        public static IServiceCollection AddS3(this IServiceCollection services)
        {
            services.AddSingleton<IAmazonS3>(sp =>
            {
                var s3Options = sp.GetRequiredService<IOptions<S3Options>>().Value;

                var awsOptions = new AWSOptions
                {
                    Credentials = new BasicAWSCredentials(s3Options.AccessKey, s3Options.SecretKey),
                    Region = RegionEndpoint.GetBySystemName(s3Options.Region)
                };
                return new AmazonS3Client(awsOptions.Credentials, awsOptions.Region);
            });

            services.AddScoped<IFileStorageRepository, FileStorageRepository>();

            return services;
        }
    }
}
