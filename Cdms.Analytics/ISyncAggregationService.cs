namespace Cdms.Analytics;

public interface ISyncAggregationService
{
    public Task<ByDateResult[]> GetImportNotificationLinks();
}