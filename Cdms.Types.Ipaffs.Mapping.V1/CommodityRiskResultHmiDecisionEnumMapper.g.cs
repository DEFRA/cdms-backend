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

public static class CommodityRiskResultHmiDecisionEnumMapper
{
public static Cdms.Model.Ipaffs.CommodityRiskResultHmiDecisionEnum? Map(Cdms.Types.Ipaffs.CommodityRiskResultHmiDecisionEnum? from)
{
if(from == null)
{
return default!;
}
return from switch
{
Cdms.Types.Ipaffs.CommodityRiskResultHmiDecisionEnum.Required => Cdms.Model.Ipaffs.CommodityRiskResultHmiDecisionEnum.Required,
    Cdms.Types.Ipaffs.CommodityRiskResultHmiDecisionEnum.Notrequired => Cdms.Model.Ipaffs.CommodityRiskResultHmiDecisionEnum.Notrequired,
     
};
}
        

}

