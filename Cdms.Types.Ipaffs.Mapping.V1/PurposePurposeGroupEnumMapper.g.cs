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

public static class PurposePurposeGroupEnumMapper
{
    public static Cdms.Model.Ipaffs.PurposePurposeGroupEnum? Map(Cdms.Types.Ipaffs.PurposePurposeGroupEnum? from)
    {
        if (from == null)
        {
            return default!;
        }

        return from switch
        {
            Cdms.Types.Ipaffs.PurposePurposeGroupEnum.ForImport => Cdms.Model.Ipaffs.PurposePurposeGroupEnum.ForImport,
            Cdms.Types.Ipaffs.PurposePurposeGroupEnum.ForNONConformingConsignments => Cdms.Model.Ipaffs
                .PurposePurposeGroupEnum.ForNONConformingConsignments,
            Cdms.Types.Ipaffs.PurposePurposeGroupEnum.ForTranshipmentTo => Cdms.Model.Ipaffs.PurposePurposeGroupEnum
                .ForTranshipmentTo,
            Cdms.Types.Ipaffs.PurposePurposeGroupEnum.ForTransitTo3rdCountry => Cdms.Model.Ipaffs
                .PurposePurposeGroupEnum.ForTransitTo3rdCountry,
            Cdms.Types.Ipaffs.PurposePurposeGroupEnum.ForReImport => Cdms.Model.Ipaffs.PurposePurposeGroupEnum
                .ForReImport,
            Cdms.Types.Ipaffs.PurposePurposeGroupEnum.ForPrivateImport => Cdms.Model.Ipaffs.PurposePurposeGroupEnum
                .ForPrivateImport,
            Cdms.Types.Ipaffs.PurposePurposeGroupEnum.ForTransferTo => Cdms.Model.Ipaffs.PurposePurposeGroupEnum
                .ForTransferTo,
            Cdms.Types.Ipaffs.PurposePurposeGroupEnum.ForImportReConformityCheck => Cdms.Model.Ipaffs
                .PurposePurposeGroupEnum.ForImportReConformityCheck,
            Cdms.Types.Ipaffs.PurposePurposeGroupEnum.ForImportNonInternalMarket => Cdms.Model.Ipaffs
                .PurposePurposeGroupEnum.ForImportNonInternalMarket,

            _ => throw new ArgumentOutOfRangeException(nameof(from), from, null)
        };
    }


}


