using Cdms.Backend.Data;
using Cdms.Model.Extensions;
using Microsoft.Extensions.Logging;

namespace Cdms.Analytics;

public class SyncAggregationService(IMongoDbContext context, ILogger<SyncAggregationService> logger) : ISyncAggregationService
{
    public async Task<ByDateResult[]> GetImportNotificationLinks()
    {
        var s = context
            .Notifications
            .GroupBy(n => new { Matched = n.Relationships.Movements.Matched.HasValue && n.Relationships.Movements.Matched.Value, n.CreatedSource!.Value.Date })
            // .SelectMany(g => new ByDateResult()[])
            .AsEnumerable()
            .Select(r => new ByDateResult()
            {
                Date = DateOnly.FromDateTime(r.Key.Date),
                BucketVariables = new Dictionary<string, string>() { { "Matched", r.Key.Matched.ToString() } },
                Value = r.Count()
            })
            .ToArray();
        
        logger.LogInformation(s.ToJsonString());
        return s;
        // context.Notifications.GroupBy(n => n.Relationships )
        // return [new ByDateResult() { Date = DateOnly.FromDateTime(DateTime.Today)}];
    }
}