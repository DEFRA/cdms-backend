using System.Collections;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using Cdms.Backend.Data;
using Cdms.Common.Extensions;
using Cdms.Model.Extensions;
using Cdms.Model.Ipaffs;
using Cdms.Model;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;

using Cdms.Analytics.Extensions;

namespace Cdms.Analytics;

public class MovementsAggregationService(IMongoDbContext context, ILogger<MovementsAggregationService> logger) : IMovementsAggregationService
{
    
    /// <summary>
    /// Aggregates movements by createdSource and returns counts by date period. Could be refactored to use a generic/interface in time
    /// </summary>
    /// <param name="from">Time period to search from (inclusive)</param>
    /// <param name="to">Time period to search to (exclusive)</param>
    /// <param name="aggregateBy">Aggregate by day/hour</param>
    /// <returns></returns>
    public Task<Dataset[]> ByCreated(DateTime from, DateTime to, AggregationPeriod aggregateBy = AggregationPeriod.Day)
    {
        var dateRange = AnalyticsHelpers.CreateDateRange(from, to, aggregateBy);
        
        Expression<Func<Movement, bool>> matchFilter = n =>
            n.CreatedSource >= from && n.CreatedSource < to;

        string CreateDatasetName(BsonDocument b) => AnalyticsHelpers.GetLinkedName(b["_id"]["linked"].ToBoolean());

        return Aggregate(dateRange, CreateDatasetName, matchFilter, "$createdSource", aggregateBy);
    }

    /// <summary>
    /// Aggregates movements by arrival date and returns counts by date period. Could be refactored to use a generic/interface in time
    /// </summary>
    /// <param name="from">Time period to search from (inclusive)</param>
    /// <param name="to">Time period to search to (exclusive)</param>
    /// <param name="aggregateBy">Aggregate by day/hour</param>
    /// <returns></returns>
    public Task<Dataset[]> ByArrival(DateTime from, DateTime to, AggregationPeriod aggregateBy = AggregationPeriod.Day)
    {
        var dateRange = AnalyticsHelpers.CreateDateRange(from, to, aggregateBy);
        
        Expression<Func<Movement, bool>> matchFilter = n =>
            n.CreatedSource >= from && n.CreatedSource < to;

        string CreateDatasetName(BsonDocument b) => AnalyticsHelpers.GetLinkedName(b["_id"]["linked"].ToBoolean());

        return Aggregate(dateRange, CreateDatasetName, matchFilter, "$arrivesAt", aggregateBy);
    }

    public Task<PieChartDataset> ByStatus(DateTime from, DateTime to)
    {
        var data = context
            .Movements
            .Where(n => n.CreatedSource >= from && n.CreatedSource < to)
            .GroupBy(m => m.Relationships.Notifications.Data.Count > 0)
            .Select(g => new { g.Key, Count = g.Count() })
            .ToDictionary(g => AnalyticsHelpers.GetLinkedName(g.Key), g => g.Count);
            
        return Task.FromResult(new PieChartDataset()
        {
            Values =  new string[] { "Linked", "Not Linked" }.ToDictionary(title => title, title => data.GetValueOrDefault(title, 0))
        });
    }
    
    private Task<Dataset[]> Aggregate(DateTime[] dateRange, Func<BsonDocument, string> createDatasetName, Expression<Func<Movement, bool>> filter, string dateField, AggregationPeriod aggregateBy)
    {
        var comparer = Comparer<ByDateTimeResult>.Create((d1, d2) => d1.Period.CompareTo(d2.Period));

        var truncateBy = aggregateBy == AggregationPeriod.Hour ? "hour" : "day";
        
        ProjectionDefinition<Movement> projection = "{linked:{ $ne: [0, { $size: '$relationships.notifications.data'}]}, dateToUse: { $dateTrunc: { date: '" + dateField + "', unit: '" + truncateBy + "'}}}";
        
        // First aggregate the dataset by whether its matched and the date it arrives. Count the number in each bucket.
        ProjectionDefinition<BsonDocument> group = "{_id: { linked: '$linked', dateToUse: '$dateToUse' }, count: { $count: { } }}";
        
        // Then further group by whether its matched and get the count to give us the structure we need in our chart
        ProjectionDefinition<BsonDocument> datasetGroup = "{_id: { linked: '$_id.linked'}, dates: { $push: { dateToUse: '$_id.dateToUse', count: '$count' }}}";

        var mongoResult = context
            .Movements
            .Aggregate()
            .Match(filter)
            .Project(projection)
            .Group(group)
            .Group(datasetGroup)
            .ToList()
            .ToDictionary(createDatasetName, b => b);

        var output = new string[] { "Linked", "Not Linked" }
            .Select(title =>
            {
                var dates = mongoResult
                    .TryGetValue(title, out var b)
                    ? b["dates"].AsBsonArray
                        .ToDictionary(AnalyticsHelpers.AggregateDateCreator, d => d["count"].AsInt32)
                    : [];
                
                return new Dataset(title)
                {
                    Periods = dateRange.Select(resultDate =>
                            new ByDateTimeResult()
                            {
                                Period = resultDate, Value = dates.GetValueOrDefault(resultDate, 0)
                            })
                        .Order(comparer)
                        .ToList()
                };
            })
            .OrderBy(d => d.Name)
            .ToArray();
        
        logger.LogDebug("Aggregated Data {result}", output.ToList().ToJsonString());
        
        return Task.FromResult(output);
    }
}