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

public static class CommoditiesCommodityIntendedForEnumMapper
{
public static Cdms.Model.Ipaffs.CommoditiesCommodityIntendedForEnum? Map(Cdms.Types.Ipaffs.CommoditiesCommodityIntendedForEnum? from)
{
if(from == null)
{
return default!;
}
return from switch
{
Cdms.Types.Ipaffs.CommoditiesCommodityIntendedForEnum.Human => Cdms.Model.Ipaffs.CommoditiesCommodityIntendedForEnum.Human,
    Cdms.Types.Ipaffs.CommoditiesCommodityIntendedForEnum.Feedingstuff => Cdms.Model.Ipaffs.CommoditiesCommodityIntendedForEnum.Feedingstuff,
    Cdms.Types.Ipaffs.CommoditiesCommodityIntendedForEnum.Further => Cdms.Model.Ipaffs.CommoditiesCommodityIntendedForEnum.Further,
    Cdms.Types.Ipaffs.CommoditiesCommodityIntendedForEnum.Other => Cdms.Model.Ipaffs.CommoditiesCommodityIntendedForEnum.Other,

    _ => throw new ArgumentOutOfRangeException(nameof(from), from, null)
};
}
        

}


