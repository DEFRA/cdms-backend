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

public static class ControlAuthorityMapper
{
	public static Cdms.Model.Ipaffs.ControlAuthority Map(Cdms.Types.Ipaffs.ControlAuthority from)
	{
	if(from is null)
	{
		return default!;
	}
		var to = new Cdms.Model.Ipaffs.ControlAuthority ();
to.OfficialVeterinarian = OfficialVeterinarianMapper.Map(from?.OfficialVeterinarian);
                to.CustomsReferenceNo = from.CustomsReferenceNo;
            to.ContainerResealed = from.ContainerResealed;
            to.NewSealNumber = from.NewSealNumber;
            to.IuuFishingReference = from.IuuFishingReference;
            to.IuuCheckRequired = from.IuuCheckRequired;
            to.IuuOption = ControlAuthorityIuuOptionEnumMapper.Map(from?.IuuOption);
                	return to;
	}
}
