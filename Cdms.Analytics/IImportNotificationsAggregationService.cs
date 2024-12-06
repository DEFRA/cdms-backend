namespace Cdms.Analytics;

public interface IImportNotificationsAggregationService
{
    public Task<MultiSeriesDatetimeDataset[]> ByCreated(DateTime from, DateTime to, AggregationPeriod aggregateBy = AggregationPeriod.Day);
    public Task<MultiSeriesDatetimeDataset[]> ByArrival(DateTime from, DateTime to, AggregationPeriod aggregateBy = AggregationPeriod.Day);
    public Task<SingeSeriesDataset> ByStatus(DateTime from, DateTime to);
    public Task<MultiSeriesDataset[]> ByCommodityCount(DateTime from, DateTime to);
}