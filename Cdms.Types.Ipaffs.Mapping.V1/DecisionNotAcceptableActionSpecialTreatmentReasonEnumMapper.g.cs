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

public static class DecisionNotAcceptableActionSpecialTreatmentReasonEnumMapper
{
public static Cdms.Model.Ipaffs.DecisionNotAcceptableActionSpecialTreatmentReasonEnum? Map(Cdms.Types.Ipaffs.DecisionNotAcceptableActionSpecialTreatmentReasonEnum? from)
{
if(from == null)
{
return default!;
}
return from switch
{
Cdms.Types.Ipaffs.DecisionNotAcceptableActionSpecialTreatmentReasonEnum.ContaminatedProducts => Cdms.Model.Ipaffs.DecisionNotAcceptableActionSpecialTreatmentReasonEnum.ContaminatedProducts,
    Cdms.Types.Ipaffs.DecisionNotAcceptableActionSpecialTreatmentReasonEnum.InterceptedPart => Cdms.Model.Ipaffs.DecisionNotAcceptableActionSpecialTreatmentReasonEnum.InterceptedPart,
    Cdms.Types.Ipaffs.DecisionNotAcceptableActionSpecialTreatmentReasonEnum.PackagingMaterial => Cdms.Model.Ipaffs.DecisionNotAcceptableActionSpecialTreatmentReasonEnum.PackagingMaterial,
    Cdms.Types.Ipaffs.DecisionNotAcceptableActionSpecialTreatmentReasonEnum.Other => Cdms.Model.Ipaffs.DecisionNotAcceptableActionSpecialTreatmentReasonEnum.Other,

    _ => throw new ArgumentOutOfRangeException(nameof(from), from, null)
};
}
        

}


