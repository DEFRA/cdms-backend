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

public static class IpaffsInspectionCheckStatusEnumMapper
{
public static Cdms.Model.Ipaffs.IpaffsInspectionCheckStatusEnum? Map(Cdms.Types.Ipaffs.IpaffsInspectionCheckStatusEnum? from)
{
if(from == null)
{
return default!;
}
return from switch
{
Cdms.Types.Ipaffs.IpaffsInspectionCheckStatusEnum.ToDo => Cdms.Model.Ipaffs.IpaffsInspectionCheckStatusEnum.ToDo,
    Cdms.Types.Ipaffs.IpaffsInspectionCheckStatusEnum.Compliant => Cdms.Model.Ipaffs.IpaffsInspectionCheckStatusEnum.Compliant,
    Cdms.Types.Ipaffs.IpaffsInspectionCheckStatusEnum.AutoCleared => Cdms.Model.Ipaffs.IpaffsInspectionCheckStatusEnum.AutoCleared,
    Cdms.Types.Ipaffs.IpaffsInspectionCheckStatusEnum.NonCompliant => Cdms.Model.Ipaffs.IpaffsInspectionCheckStatusEnum.NonCompliant,
    Cdms.Types.Ipaffs.IpaffsInspectionCheckStatusEnum.NotInspected => Cdms.Model.Ipaffs.IpaffsInspectionCheckStatusEnum.NotInspected,
    Cdms.Types.Ipaffs.IpaffsInspectionCheckStatusEnum.ToBeInspected => Cdms.Model.Ipaffs.IpaffsInspectionCheckStatusEnum.ToBeInspected,
    Cdms.Types.Ipaffs.IpaffsInspectionCheckStatusEnum.Hold => Cdms.Model.Ipaffs.IpaffsInspectionCheckStatusEnum.Hold,
     
};
}
        

}


