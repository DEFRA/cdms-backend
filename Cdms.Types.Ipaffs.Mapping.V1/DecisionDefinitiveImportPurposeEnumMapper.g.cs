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

public static class DecisionDefinitiveImportPurposeEnumMapper
{
public static Cdms.Model.Ipaffs.DecisionDefinitiveImportPurposeEnum? Map(Cdms.Types.Ipaffs.DecisionDefinitiveImportPurposeEnum? from)
{
if(from == null)
{
return default!;
}
return from switch
{
Cdms.Types.Ipaffs.DecisionDefinitiveImportPurposeEnum.Slaughter => Cdms.Model.Ipaffs.DecisionDefinitiveImportPurposeEnum.Slaughter,
    Cdms.Types.Ipaffs.DecisionDefinitiveImportPurposeEnum.Approvedbodies => Cdms.Model.Ipaffs.DecisionDefinitiveImportPurposeEnum.Approvedbodies,
    Cdms.Types.Ipaffs.DecisionDefinitiveImportPurposeEnum.Quarantine => Cdms.Model.Ipaffs.DecisionDefinitiveImportPurposeEnum.Quarantine,
     
};
}
        

}

