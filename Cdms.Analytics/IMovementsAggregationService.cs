namespace Cdms.Analytics;

public interface IMovementsAggregationService
{
    public Task<MultiSeriesDatetimeDataset[]> ByCreated(DateTime from, DateTime to, AggregationPeriod aggregateBy = AggregationPeriod.Day);
    public Task<MultiSeriesDatetimeDataset[]> ByArrival(DateTime from, DateTime to, AggregationPeriod aggregateBy = AggregationPeriod.Day);
    public Task<SingeSeriesDataset> ByStatus(DateTime from, DateTime to);
    public Task<MultiSeriesDataset[]> ByItemCount(DateTime from, DateTime to);
    public Task<MultiSeriesDataset[]> ByUniqueDocumentReferenceCount(DateTime from, DateTime to);
    public Task<SingeSeriesDataset> UniqueDocumentReferenceByMovementCount(DateTime from, DateTime to);
}