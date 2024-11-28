using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Cdms.Metrics;

public class LinkingMetrics
{
    readonly Histogram<double> duration;
    readonly Counter<long> total;
    readonly Counter<long> faulted;

    public LinkingMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create(MetricNames.MeterName);
        duration = meter.CreateHistogram<double>("messaging.memory.time_queued", "ms", "Elapsed time spent consuming a message, in millis");

        total = meter.CreateCounter<long>("messaging.memory.incoming", description: "Number of messages incoming");
        faulted = meter.CreateCounter<long>("messaging.memory.outgoing", description: "Number of messages outgoing");
        
    }
    public void Faulted(Exception exception)
    {
        var tagList = BuildTags();
        tagList.Add(MetricNames.CommonTags.ExceptionType, exception.GetType().Name);
        faulted.Add(1, tagList);

    }

    public void Linked<T>(long delta)
    {
        var tagList = BuildTags();
        tagList.Add("cdms.linking.resource_type", typeof(T).Name);
        total.Add(delta, tagList);
    }

    public void Completed(double milliseconds)
    {
        var tagList = BuildTags();
        duration.Record(milliseconds, tagList);
    }

    private static TagList BuildTags()
    {
        return new TagList
        {
            { MetricNames.CommonTags.Service, Process.GetCurrentProcess().ProcessName }
        };
    }
}