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

public static class DecisionNotAcceptableActionEnumMapper
{
public static Cdms.Model.Ipaffs.DecisionNotAcceptableActionEnum? Map(Cdms.Types.Ipaffs.DecisionNotAcceptableActionEnum? from)
{
if(from == null)
{
return default!;
}
return from switch
{
Cdms.Types.Ipaffs.DecisionNotAcceptableActionEnum.Slaughter => Cdms.Model.Ipaffs.DecisionNotAcceptableActionEnum.Slaughter,
    Cdms.Types.Ipaffs.DecisionNotAcceptableActionEnum.Reexport => Cdms.Model.Ipaffs.DecisionNotAcceptableActionEnum.Reexport,
    Cdms.Types.Ipaffs.DecisionNotAcceptableActionEnum.Euthanasia => Cdms.Model.Ipaffs.DecisionNotAcceptableActionEnum.Euthanasia,
    Cdms.Types.Ipaffs.DecisionNotAcceptableActionEnum.Redispatching => Cdms.Model.Ipaffs.DecisionNotAcceptableActionEnum.Redispatching,
    Cdms.Types.Ipaffs.DecisionNotAcceptableActionEnum.Destruction => Cdms.Model.Ipaffs.DecisionNotAcceptableActionEnum.Destruction,
    Cdms.Types.Ipaffs.DecisionNotAcceptableActionEnum.Transformation => Cdms.Model.Ipaffs.DecisionNotAcceptableActionEnum.Transformation,
    Cdms.Types.Ipaffs.DecisionNotAcceptableActionEnum.Other => Cdms.Model.Ipaffs.DecisionNotAcceptableActionEnum.Other,
    Cdms.Types.Ipaffs.DecisionNotAcceptableActionEnum.EntryRefusal => Cdms.Model.Ipaffs.DecisionNotAcceptableActionEnum.EntryRefusal,
    Cdms.Types.Ipaffs.DecisionNotAcceptableActionEnum.QuarantineImposed => Cdms.Model.Ipaffs.DecisionNotAcceptableActionEnum.QuarantineImposed,
    Cdms.Types.Ipaffs.DecisionNotAcceptableActionEnum.SpecialTreatment => Cdms.Model.Ipaffs.DecisionNotAcceptableActionEnum.SpecialTreatment,
    Cdms.Types.Ipaffs.DecisionNotAcceptableActionEnum.IndustrialProcessing => Cdms.Model.Ipaffs.DecisionNotAcceptableActionEnum.IndustrialProcessing,
    Cdms.Types.Ipaffs.DecisionNotAcceptableActionEnum.ReDispatch => Cdms.Model.Ipaffs.DecisionNotAcceptableActionEnum.ReDispatch,
    Cdms.Types.Ipaffs.DecisionNotAcceptableActionEnum.UseForOtherPurposes => Cdms.Model.Ipaffs.DecisionNotAcceptableActionEnum.UseForOtherPurposes,

    _ => throw new ArgumentOutOfRangeException(nameof(from), from, null)
};
}
        

}


