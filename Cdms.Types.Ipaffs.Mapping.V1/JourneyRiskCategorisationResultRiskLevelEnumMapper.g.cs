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

public static class JourneyRiskCategorisationResultRiskLevelEnumMapper
{
    public static Cdms.Model.Ipaffs.JourneyRiskCategorisationResultRiskLevelEnum? Map(
        Cdms.Types.Ipaffs.JourneyRiskCategorisationResultRiskLevelEnum? from)
    {
        if (from == null)
        {
            return default!;
        }

        return from switch
        {
            Cdms.Types.Ipaffs.JourneyRiskCategorisationResultRiskLevelEnum.High => Cdms.Model.Ipaffs
                .JourneyRiskCategorisationResultRiskLevelEnum.High,
            Cdms.Types.Ipaffs.JourneyRiskCategorisationResultRiskLevelEnum.Medium => Cdms.Model.Ipaffs
                .JourneyRiskCategorisationResultRiskLevelEnum.Medium,
            Cdms.Types.Ipaffs.JourneyRiskCategorisationResultRiskLevelEnum.Low => Cdms.Model.Ipaffs
                .JourneyRiskCategorisationResultRiskLevelEnum.Low,

            _ => throw new ArgumentOutOfRangeException(nameof(from), from, null)
        };
    }


}


