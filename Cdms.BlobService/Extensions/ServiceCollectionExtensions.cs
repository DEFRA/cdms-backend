using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Cdms.BlobService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlobStorage(this IServiceCollection services,
            IConfiguration configuration)
        {
            var config = configuration.GetSection(BlobServiceOptions.SectionName);
            
            services.AddOptions<BlobServiceOptions>()
                .Bind(config)
                .ValidateDataAnnotations();

            var blobOptions = config.Get<BlobServiceOptions>()!;
            
            if (blobOptions.CacheReadEnabled || blobOptions.CacheWriteEnabled)
            {
                services.AddKeyedSingleton<IBlobService, BlobService>("base");
                services.AddSingleton<IBlobService, CachingBlobService>();
            }
            else
            {
                services.AddSingleton<IBlobService, BlobService>();
            }
            
            services.AddSingleton<IBlobServiceClientFactory, BlobServiceClientFactory>();
            
            return services;
        }
    }
}