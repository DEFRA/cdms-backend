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

public static class DecisionNotAcceptableActionIndustrialProcessingReasonEnumMapper
{
public static Cdms.Model.Ipaffs.DecisionNotAcceptableActionIndustrialProcessingReasonEnum? Map(Cdms.Types.Ipaffs.DecisionNotAcceptableActionIndustrialProcessingReasonEnum? from)
{
if(from == null)
{
return default!;
}
return from switch
{
Cdms.Types.Ipaffs.DecisionNotAcceptableActionIndustrialProcessingReasonEnum.ContaminatedProducts => Cdms.Model.Ipaffs.DecisionNotAcceptableActionIndustrialProcessingReasonEnum.ContaminatedProducts,
    Cdms.Types.Ipaffs.DecisionNotAcceptableActionIndustrialProcessingReasonEnum.InterceptedPart => Cdms.Model.Ipaffs.DecisionNotAcceptableActionIndustrialProcessingReasonEnum.InterceptedPart,
    Cdms.Types.Ipaffs.DecisionNotAcceptableActionIndustrialProcessingReasonEnum.PackagingMaterial => Cdms.Model.Ipaffs.DecisionNotAcceptableActionIndustrialProcessingReasonEnum.PackagingMaterial,
    Cdms.Types.Ipaffs.DecisionNotAcceptableActionIndustrialProcessingReasonEnum.Other => Cdms.Model.Ipaffs.DecisionNotAcceptableActionIndustrialProcessingReasonEnum.Other,
     
};
}
        

}

