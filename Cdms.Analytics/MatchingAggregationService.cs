using System.Collections;
using Microsoft.Extensions.Logging;
using System.Data.Entity;

using Cdms.Backend.Data;
using Cdms.Common.Extensions;
using Cdms.Model.Extensions;
using Cdms.Model.Ipaffs;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Cdms.Analytics;

public class MatchingAggregationService(IMongoDbContext context, ILogger<MatchingAggregationService> logger) : IMatchingAggregationService
{
    public Task<ByDateResult[]> GetImportNotificationMatchingByCreated()
    {
        var s = context
            .Notifications
            .GroupBy(n => new
            {
                Matched = n.Relationships.Movements.Matched.HasValue && n.Relationships.Movements.Matched.Value,
                n.CreatedSource!.Value.Date,
                n.ImportNotificationType
            })
            .AsEnumerable()
            .Select(r => new ByDateResult()
            {
                Date = r.Key.Date.ToDate(),
                BucketVariables = new Dictionary<string, string>()
                {
                    {
                        "Matched", r.Key.Matched.ToString()
                    },
                    {
                        "ChedType", r.Key.ImportNotificationType.ToString()!
                    }
                },
                Value = r.Count()
            })
            .OrderBy(r => r.Date)
            .ThenBy(r => r.BucketVariables["Matched"])
            .ThenBy(r => r.BucketVariables["ChedType"])
            .ToArray();

        return Task.FromResult(s);
    }

    public Task<Dataset[]> GetImportNotificationMatchingByArrival()
    {   
        ProjectionDefinition<ImportNotification> projection = "{matched:{ $ifNull: [ '$relationships.movements.matched', false]}, 'importNotificationType':1, arrivedOn: { $dateTrunc: { date: '$partOne.arrivedOn', unit: 'day'}}}";
        
        // First aggregate the dataset by chedtype, whether its matched and the date it arrives. Count the number in each bucket.
        ProjectionDefinition<BsonDocument> group = "{_id: { matched: '$matched', importNotificationType: '$importNotificationType', arrivedOn: '$arrivedOn' }, count: { $count: { } }}";
        
        // Then further group by chedtype & whether its matched to give us the structure we need in our chart
        ProjectionDefinition<BsonDocument> datasetGroup = "{_id: { importNotificationType: '$_id.importNotificationType', matched: '$_id.matched'}, dates: { $push: { arrivedOn: '$_id.arrivedOn', count: '$count' }}}";

        // Ideally we'd sort in the aggregation framework but doesn't seem to work
        // SortDefinition<BsonDocument> sort = "{ importNotificationType: 1, arrivedOn: 1, matched: 1 }";

        var comparer = Comparer<ByDateResult>.Create((d1, d2) => d1.Date.CompareTo(d2.Date));

        var mongoResult = context
            .Notifications
            .Aggregate()
            .Project(projection)
            .Group(group)
            .Group(datasetGroup)
            .ToList()
            .Select(b =>
                new Dataset(b["_id"]["importNotificationType"].ToString()!, b["_id"]["matched"].ToBoolean())
                {
                    Dates = b["dates"].AsBsonArray.Select(a => new ByDateResult()
                        {
                            Date = a["arrivedOn"].ToUniversalTime().ToDate(), Value = a["count"].AsInt32
                        })
                        .Order(comparer)
                        .ToList()
                }
            )
            .OrderBy(d => d.Name)
            .ToArray();
        
        logger.LogDebug("Aggregated Data {result}", mongoResult.ToList().ToJsonString());
        
        return Task.FromResult(mongoResult);

    }
}