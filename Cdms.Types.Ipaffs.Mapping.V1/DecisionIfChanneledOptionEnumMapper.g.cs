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

public static class DecisionIfChanneledOptionEnumMapper
{
public static Cdms.Model.Ipaffs.DecisionIfChanneledOptionEnum? Map(Cdms.Types.Ipaffs.DecisionIfChanneledOptionEnum? from)
{
if(from == null)
{
return default!;
}
return from switch
{
Cdms.Types.Ipaffs.DecisionIfChanneledOptionEnum.Article8 => Cdms.Model.Ipaffs.DecisionIfChanneledOptionEnum.Article8,
    Cdms.Types.Ipaffs.DecisionIfChanneledOptionEnum.Article15 => Cdms.Model.Ipaffs.DecisionIfChanneledOptionEnum.Article15,

    _ => throw new ArgumentOutOfRangeException(nameof(from), from, null)
};
}
        

}


