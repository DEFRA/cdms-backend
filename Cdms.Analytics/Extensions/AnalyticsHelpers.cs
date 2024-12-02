using Cdms.Common.Extensions;
using Cdms.Model.Extensions;
using MongoDB.Bson;
namespace Cdms.Analytics.Extensions;

public static class AnalyticsMetricNames
{
    public const string MeterName = "Cdms.Backend.Analytics";
    public const string MetricPrefix = "cdms.service.analytics";

    public static class CommonTags
    {
        public const string Service = "cdms.service.anayltics";
        public const string ExceptionType = "cdms.exception_type";
        public const string MessageType = "cdms.message_type";
    }
}

public static class AnalyticsHelpers
{
    internal static DateTime AggregateDateCreator(this BsonValue b) => b["dateToUse"].BsonType != BsonType.Null ? b["dateToUse"].ToUniversalTime() : DateTime.MinValue;

    internal static string GetLinkedName(bool linked, string type)
    {
        return $"{type} {GetLinkedName(linked)}";
    }
    internal static string GetLinkedName(bool linked)
    {
        return linked ? "Linked" : "Not Linked";
    }
    
}