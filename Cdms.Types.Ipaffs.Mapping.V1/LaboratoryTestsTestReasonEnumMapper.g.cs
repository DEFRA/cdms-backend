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

public static class LaboratoryTestsTestReasonEnumMapper
{
    public static Cdms.Model.Ipaffs.LaboratoryTestsTestReasonEnum? Map(
        Cdms.Types.Ipaffs.LaboratoryTestsTestReasonEnum? from)
    {
        if (from == null)
        {
            return default!;
        }

        return from switch
        {
            Cdms.Types.Ipaffs.LaboratoryTestsTestReasonEnum.Random => Cdms.Model.Ipaffs.LaboratoryTestsTestReasonEnum
                .Random,
            Cdms.Types.Ipaffs.LaboratoryTestsTestReasonEnum.Suspicious => Cdms.Model.Ipaffs
                .LaboratoryTestsTestReasonEnum.Suspicious,
            Cdms.Types.Ipaffs.LaboratoryTestsTestReasonEnum.ReEnforced => Cdms.Model.Ipaffs
                .LaboratoryTestsTestReasonEnum.ReEnforced,
            Cdms.Types.Ipaffs.LaboratoryTestsTestReasonEnum.IntensifiedControls => Cdms.Model.Ipaffs
                .LaboratoryTestsTestReasonEnum.IntensifiedControls,
            Cdms.Types.Ipaffs.LaboratoryTestsTestReasonEnum.Required => Cdms.Model.Ipaffs.LaboratoryTestsTestReasonEnum
                .Required,
            Cdms.Types.Ipaffs.LaboratoryTestsTestReasonEnum.LatentInfectionSampling => Cdms.Model.Ipaffs
                .LaboratoryTestsTestReasonEnum.LatentInfectionSampling,

            _ => throw new ArgumentOutOfRangeException(nameof(from), from, null)
        };
    }


}


