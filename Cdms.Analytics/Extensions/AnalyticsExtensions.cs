using System.Collections;
using System.Linq.Expressions;
using Cdms.Backend.Data;
using Cdms.Model.Data;
using Cdms.Model.Extensions;
using Cdms.Model.Ipaffs;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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

    public static string MetricsKey(this MultiSeriesDatetimeDataset ds)
    {
        return ds.Name.Replace(" ", "-").ToLower();
    }

    public static int Periods(this TimeSpan t, AggregationPeriod aggregateBy) => Convert.ToInt32(aggregateBy == AggregationPeriod.Hour ? t.TotalHours : t.TotalDays);
    
    public static DateTime Increment(this DateTime d, int offset, AggregationPeriod aggregateBy) => aggregateBy == AggregationPeriod.Hour ? d.AddHours(offset) : d.AddDays(offset);

    public static Dictionary<string, BsonDocument> GetAggregatedRecordsDictionary<T>(
        this IMongoCollectionSet<T> collection,
        FilterDefinition<T> filter,
        ProjectionDefinition<T> projection,
        ProjectionDefinition<BsonDocument> group,
        ProjectionDefinition<BsonDocument> datasetGroup,
        Func<BsonDocument, string> createDatasetName) where T : IDataEntity
    {
        return collection
            .Aggregate()
            .Match(filter)
            .Project(projection)
            .Group(group)
            .Group(datasetGroup)
            .ToList()
            .ToDictionary(createDatasetName, b => b);
    }

    public static Dictionary<DateTime, int> GetNamedSetAsDict(this Dictionary<string, BsonDocument> records, string title)
    {
        return records
            .TryGetValue(title, out var b)
            ? b["dates"].AsBsonArray
                .ToDictionary(AnalyticsHelpers.AggregateDateCreator, d => d["count"].AsInt32)
            : [];
    }

    public static MultiSeriesDatetimeDataset AsDataset(this Dictionary<string, BsonDocument> records, DateTime[] dateRange, string title)
    {
        var dates = records.GetNamedSetAsDict(title);
        return new MultiSeriesDatetimeDataset(title)
        {
            Periods = dateRange
                .Select(resultDate =>
                    new ByDateTimeResult()
                    {
                        Period = resultDate, Value = dates.GetValueOrDefault(resultDate, 0)
                    })
                .Order(AnalyticsHelpers.byDateTimeResultComparer)
                .ToList()
        };
    }

    public static T[] AsOrderedArray<T, TKey>(this IEnumerable<T> en, Func<T, TKey> keySelector)
    {
        return en
            .OrderBy(keySelector)
            .ToArray();
    }

    internal static IEnumerable<IGrouping<TKey, TSource>> Execute<TSource, TKey>(this IQueryable<IGrouping<TKey, TSource>> source, ILogger logger)
    {
        
        try
        {
            var aggregatedData = source.ToList();
            return aggregatedData;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error querying Mongo : {message}", ex.Message);
            throw new AnalyticsException("Error querying Mongo", ex);
        }
        finally 
        {
            logger.LogExecutedMongoString(source);
        }
    }
    
    internal static IEnumerable<TSource> Execute<TSource>(
        this IQueryable<TSource> source, ILogger logger) 
    {
        
        try
        {
            var aggregatedData = source.ToList();
            return aggregatedData;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error querying Mongo : {message}", ex.Message);
            throw new AnalyticsException("Error querying Mongo", ex);
        }
        finally 
        {
            logger.LogExecutedMongoString(source);
        }
    }

    /// <summary>
    /// Gets the executed query details to allow issues to be reproduced
    /// I'm sure this could be made cleaner but should do for now.
    /// </summary>
    /// <param name="logger">Where to write the log</param>
    /// <param name="source">A mongo query that has already executed</param>
    private static void LogExecutedMongoString(this ILogger logger, IQueryable source)
    {
        var stages = ((IMongoQueryProvider)source.Provider).LoggedStages;

        var query = "[" + String.Join(",", stages.Select(s => s.ToString()).ToArray()) +"]";
        
        logger.LogInformation(query);
    }
}