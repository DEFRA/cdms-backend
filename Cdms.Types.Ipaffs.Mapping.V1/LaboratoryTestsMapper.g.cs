//------------------------------------------------------------------------------
// <auto-generated>
	//     This code was generated from a template.
	//
	//     Manual changes to this file may cause unexpected behavior in your application.
	//     Manual changes to this file will be overwritten if the code is regenerated.
	//
//</auto-generated>
//------------------------------------------------------------------------------
#nullable enable


namespace Cdms.Types.Ipaffs.Mapping;

public static class LaboratoryTestsMapper
{
	public static Cdms.Model.Ipaffs.LaboratoryTests Map(Cdms.Types.Ipaffs.LaboratoryTests from)
	{
	if(from is null)
	{
		return default!;
	}
		var to = new Cdms.Model.Ipaffs.LaboratoryTests ();
to.TestedOn = from.TestDate;
            to.TestReason = LaboratoryTestsTestReasonEnumMapper.Map(from?.TestReason);
                to.SingleLaboratoryTests = from?.SingleLaboratoryTests?.Select(x => SingleLaboratoryTestMapper.Map(x)).ToArray();
                	return to;
	}
}
