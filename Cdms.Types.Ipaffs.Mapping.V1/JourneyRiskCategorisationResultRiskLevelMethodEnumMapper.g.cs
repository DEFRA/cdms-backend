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

public static class JourneyRiskCategorisationResultRiskLevelMethodEnumMapper
{
public static Cdms.Model.Ipaffs.JourneyRiskCategorisationResultRiskLevelMethodEnum? Map(Cdms.Types.Ipaffs.JourneyRiskCategorisationResultRiskLevelMethodEnum? from)
{
if(from == null)
{
return default!;
}
return from switch
{
Cdms.Types.Ipaffs.JourneyRiskCategorisationResultRiskLevelMethodEnum.System => Cdms.Model.Ipaffs.JourneyRiskCategorisationResultRiskLevelMethodEnum.System,
    Cdms.Types.Ipaffs.JourneyRiskCategorisationResultRiskLevelMethodEnum.User => Cdms.Model.Ipaffs.JourneyRiskCategorisationResultRiskLevelMethodEnum.User,
     
};
}
        

}


