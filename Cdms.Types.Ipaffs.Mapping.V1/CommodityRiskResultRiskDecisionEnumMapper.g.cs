//------------------------------------------------------------------------------
// <auto-generated>
    //     This code was generated from a template.
    //
    //     Manual changes to this file may cause unexpected behavior in your application.
    //     Manual changes to this file will be overwritten if the code is regenerated.
    // </auto-generated>
//------------------------------------------------------------------------------
#nullable enable
namespace Cdms.Types.Ipaffs.Mapping;

public static class CommodityRiskResultRiskDecisionEnumMapper
{
public static Cdms.Model.Ipaffs.CommodityRiskResultRiskDecisionEnum? Map(Cdms.Types.Ipaffs.CommodityRiskResultRiskDecisionEnum? from)
{
if(from == null)
{
return default!;
}
return from switch
{
Cdms.Types.Ipaffs.CommodityRiskResultRiskDecisionEnum.Required => Cdms.Model.Ipaffs.CommodityRiskResultRiskDecisionEnum.Required,
    Cdms.Types.Ipaffs.CommodityRiskResultRiskDecisionEnum.Notrequired => Cdms.Model.Ipaffs.CommodityRiskResultRiskDecisionEnum.Notrequired,
    Cdms.Types.Ipaffs.CommodityRiskResultRiskDecisionEnum.Inconclusive => Cdms.Model.Ipaffs.CommodityRiskResultRiskDecisionEnum.Inconclusive,
    Cdms.Types.Ipaffs.CommodityRiskResultRiskDecisionEnum.ReenforcedCheck => Cdms.Model.Ipaffs.CommodityRiskResultRiskDecisionEnum.ReenforcedCheck,

    _ => throw new ArgumentOutOfRangeException(nameof(from), from, null)
};
}
        

}


