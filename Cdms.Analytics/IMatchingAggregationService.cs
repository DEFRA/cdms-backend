namespace Cdms.Analytics;

public interface IMatchingAggregationService
{
    public Task<ByDateResult[]> GetImportNotificationMatchingByCreated();
    public Task<Dataset[]> GetImportNotificationMatchingByArrival();
}