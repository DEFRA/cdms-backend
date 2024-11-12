using Microsoft.Extensions.DependencyInjection;

namespace Cdms.SyncJob.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSyncJob(this IServiceCollection services)
        {
            services.AddSingleton<ISyncJobStore, SyncJobStore>();

            return services;
        }
    }
}