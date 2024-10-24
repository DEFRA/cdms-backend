using System.Text.Json.Serialization;

namespace Cdms.Types.Ipaffs;

/// <summary>
/// Holder for additional parameters of a commodity line item
/// </summary>
public partial class IpaffsCommodityComplement //
{
    public IDictionary<string, object>? AdditionalData { get; set; } = new Dictionary<string, object>();

    public IpaffsCommodityRiskResult? RiskAssesment { get; set; }

    [JsonPropertyName("checks")] public IpaffsInspectionCheck[]? Checks { get; set; }
}