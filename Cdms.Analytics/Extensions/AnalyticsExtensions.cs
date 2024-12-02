using Cdms.Model.Ipaffs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Bson;

namespace Cdms.Analytics.Extensions;

public static class AnalyticsExtensions
{
    private static readonly bool enableMetrics = true;
    public static IServiceCollection AddAnalyticsServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IImportNotificationsAggregationService, ImportNotificationsAggregationService>();
        services.AddScoped<IMovementsAggregationService, MovementsAggregationService>();

        // To revisit in future 
        if (enableMetrics)
        {
            services.TryAddScoped<ImportNotificationMetrics>();    
        }
        
        return services;
    }

    public static string MetricsKey(this Dataset ds)
    {
        return ds.Name.Replace(" ", "-").ToLower();
    }
}