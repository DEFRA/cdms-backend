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

public static class IpaffsDecisionNotAcceptableActionEntryRefusalReasonEnumMapper
{
public static Cdms.Model.Ipaffs.IpaffsDecisionNotAcceptableActionEntryRefusalReasonEnum? Map(Cdms.Types.Ipaffs.IpaffsDecisionNotAcceptableActionEntryRefusalReasonEnum? from)
{
if(from == null)
{
return default!;
}
return from switch
{
Cdms.Types.Ipaffs.IpaffsDecisionNotAcceptableActionEntryRefusalReasonEnum.ContaminatedProducts => Cdms.Model.Ipaffs.IpaffsDecisionNotAcceptableActionEntryRefusalReasonEnum.ContaminatedProducts,
    Cdms.Types.Ipaffs.IpaffsDecisionNotAcceptableActionEntryRefusalReasonEnum.InterceptedPart => Cdms.Model.Ipaffs.IpaffsDecisionNotAcceptableActionEntryRefusalReasonEnum.InterceptedPart,
    Cdms.Types.Ipaffs.IpaffsDecisionNotAcceptableActionEntryRefusalReasonEnum.PackagingMaterial => Cdms.Model.Ipaffs.IpaffsDecisionNotAcceptableActionEntryRefusalReasonEnum.PackagingMaterial,
    Cdms.Types.Ipaffs.IpaffsDecisionNotAcceptableActionEntryRefusalReasonEnum.MeansOfTransport => Cdms.Model.Ipaffs.IpaffsDecisionNotAcceptableActionEntryRefusalReasonEnum.MeansOfTransport,
    Cdms.Types.Ipaffs.IpaffsDecisionNotAcceptableActionEntryRefusalReasonEnum.Other => Cdms.Model.Ipaffs.IpaffsDecisionNotAcceptableActionEntryRefusalReasonEnum.Other,
     
};
}
        

}


