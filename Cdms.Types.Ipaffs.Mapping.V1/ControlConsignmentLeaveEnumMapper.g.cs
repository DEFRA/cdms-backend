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

public static class ControlConsignmentLeaveEnumMapper
{
public static Cdms.Model.Ipaffs.ControlConsignmentLeaveEnum? Map(Cdms.Types.Ipaffs.ControlConsignmentLeaveEnum? from)
{
if(from == null)
{
return default!;
}
return from switch
{
Cdms.Types.Ipaffs.ControlConsignmentLeaveEnum.Yes => Cdms.Model.Ipaffs.ControlConsignmentLeaveEnum.Yes,
    Cdms.Types.Ipaffs.ControlConsignmentLeaveEnum.No => Cdms.Model.Ipaffs.ControlConsignmentLeaveEnum.No,
    Cdms.Types.Ipaffs.ControlConsignmentLeaveEnum.ItHasBeenDestroyed => Cdms.Model.Ipaffs.ControlConsignmentLeaveEnum.ItHasBeenDestroyed,

    _ => throw new ArgumentOutOfRangeException(nameof(from), from, null)
};
}
        

}


