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

public static class DecisionNotAcceptableActionReDispatchReasonEnumMapper
{
public static Cdms.Model.Ipaffs.DecisionNotAcceptableActionReDispatchReasonEnum? Map(Cdms.Types.Ipaffs.DecisionNotAcceptableActionReDispatchReasonEnum? from)
{
if(from == null)
{
return default!;
}
return from switch
{
Cdms.Types.Ipaffs.DecisionNotAcceptableActionReDispatchReasonEnum.ContaminatedProducts => Cdms.Model.Ipaffs.DecisionNotAcceptableActionReDispatchReasonEnum.ContaminatedProducts,
    Cdms.Types.Ipaffs.DecisionNotAcceptableActionReDispatchReasonEnum.InterceptedPart => Cdms.Model.Ipaffs.DecisionNotAcceptableActionReDispatchReasonEnum.InterceptedPart,
    Cdms.Types.Ipaffs.DecisionNotAcceptableActionReDispatchReasonEnum.PackagingMaterial => Cdms.Model.Ipaffs.DecisionNotAcceptableActionReDispatchReasonEnum.PackagingMaterial,
    Cdms.Types.Ipaffs.DecisionNotAcceptableActionReDispatchReasonEnum.MeansOfTransport => Cdms.Model.Ipaffs.DecisionNotAcceptableActionReDispatchReasonEnum.MeansOfTransport,
    Cdms.Types.Ipaffs.DecisionNotAcceptableActionReDispatchReasonEnum.Other => Cdms.Model.Ipaffs.DecisionNotAcceptableActionReDispatchReasonEnum.Other,
     
};
}
        

}

