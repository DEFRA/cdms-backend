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

public static class PurposeForNonConformingEnumMapper
{
    public static Cdms.Model.Ipaffs.PurposeForNonConformingEnum? Map(
        Cdms.Types.Ipaffs.PurposeForNonConformingEnum? from)
    {
        if (from == null)
        {
            return default!;
        }

        return from switch
        {
            Cdms.Types.Ipaffs.PurposeForNonConformingEnum.CustomsWarehouse => Cdms.Model.Ipaffs
                .PurposeForNonConformingEnum.CustomsWarehouse,
            Cdms.Types.Ipaffs.PurposeForNonConformingEnum.FreeZoneOrFreeWarehouse => Cdms.Model.Ipaffs
                .PurposeForNonConformingEnum.FreeZoneOrFreeWarehouse,
            Cdms.Types.Ipaffs.PurposeForNonConformingEnum.ShipSupplier => Cdms.Model.Ipaffs.PurposeForNonConformingEnum
                .ShipSupplier,
            Cdms.Types.Ipaffs.PurposeForNonConformingEnum.Ship => Cdms.Model.Ipaffs.PurposeForNonConformingEnum.Ship,

            _ => throw new ArgumentOutOfRangeException(nameof(from), from, null)
        };
    }


}


