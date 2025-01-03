//------------------------------------------------------------------------------
// <auto-generated>
    //     This code was generated from a template.
    //
    //     Manual changes to this file may cause unexpected behavior in your application.
    //     Manual changes to this file will be overwritten if the code is regenerated.
    // </auto-generated>
//------------------------------------------------------------------------------
#nullable enable
namespace Cdms.Types.Gvms.Mapping;

public static class StateEnumMapper
{
    public static Cdms.Model.Gvms.StateEnum? Map(Cdms.Types.Gvms.StateEnum? from)
    {
        if (from == null)
        {
            return default!;
        }

        return from switch
        {
            Cdms.Types.Gvms.StateEnum.NotFinalisable => Cdms.Model.Gvms.StateEnum.NotFinalisable,
            Cdms.Types.Gvms.StateEnum.Open => Cdms.Model.Gvms.StateEnum.Open,
            Cdms.Types.Gvms.StateEnum.Finalised => Cdms.Model.Gvms.StateEnum.Finalised,
            Cdms.Types.Gvms.StateEnum.CheckedIn => Cdms.Model.Gvms.StateEnum.CheckedIn,
            Cdms.Types.Gvms.StateEnum.Embarked => Cdms.Model.Gvms.StateEnum.Embarked,
            Cdms.Types.Gvms.StateEnum.Completed => Cdms.Model.Gvms.StateEnum.Completed,

            _ => throw new ArgumentOutOfRangeException(nameof(from), from, null)
        };
    }


}


