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

public static class IpaffsInspectionCheckTypeEnumMapper
{
public static Cdms.Model.Ipaffs.IpaffsInspectionCheckTypeEnum? Map(Cdms.Types.Ipaffs.IpaffsInspectionCheckTypeEnum? from)
{
if(from == null)
{
return default!;
}
return from switch
{
Cdms.Types.Ipaffs.IpaffsInspectionCheckTypeEnum.PhsiDocument => Cdms.Model.Ipaffs.IpaffsInspectionCheckTypeEnum.PhsiDocument,
    Cdms.Types.Ipaffs.IpaffsInspectionCheckTypeEnum.PhsiIdentity => Cdms.Model.Ipaffs.IpaffsInspectionCheckTypeEnum.PhsiIdentity,
    Cdms.Types.Ipaffs.IpaffsInspectionCheckTypeEnum.PhsiPhysical => Cdms.Model.Ipaffs.IpaffsInspectionCheckTypeEnum.PhsiPhysical,
    Cdms.Types.Ipaffs.IpaffsInspectionCheckTypeEnum.Hmi => Cdms.Model.Ipaffs.IpaffsInspectionCheckTypeEnum.Hmi,
     
};
}
        

}


