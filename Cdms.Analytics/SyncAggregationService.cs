using Cdms.Backend.Data;

namespace Cdms.Analytics;

public class SyncAggregationService(IMongoDbContext context) : ISyncAggregationService
{
    public async Task<ByDateResult[]> GetImportNotificationLinks()
    {
        // context.Notifications.GroupBy(n => n.Relationships )
        return [new ByDateResult() { Date = DateOnly.FromDateTime(DateTime.Today)}];
    }
}