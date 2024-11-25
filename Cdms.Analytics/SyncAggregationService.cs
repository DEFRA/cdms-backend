using Cdms.Backend.Data;
using Cdms.Model.Extensions;
using Microsoft.Extensions.Logging;

namespace Cdms.Analytics;

public class SyncAggregationService(IMongoDbContext context, ILogger<SyncAggregationService> logger) : ISyncAggregationService
{
    public async Task<ByDateResult[]> GetImportNotificationsByMatchStatus()
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
                Date = DateOnly.FromDateTime(r.Key.Date),
                BucketVariables = new Dictionary<string, string>()
                {
                    {
                        "Matched", r.Key.Matched.ToString()
                    },
                    {
                        "ChedType", r.Key.ImportNotificationType.ToString()
                    }
                },
                Value = r.Count()
            })
            .ToArray();
        
        return s;
    }
}