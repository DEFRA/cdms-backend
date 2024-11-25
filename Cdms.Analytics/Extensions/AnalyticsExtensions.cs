using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cdms.Analytics.Extensions;

public static class AnalyticsExtensions
{   
    public static IServiceCollection AddServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<ISyncAggregationService, SyncAggregationService>();
        return services;
    }
}