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

public static class IpaffsInternationalTelephoneMapper
{
	public static Cdms.Model.Ipaffs.IpaffsInternationalTelephone Map(Cdms.Types.Ipaffs.IpaffsInternationalTelephone from)
	{
	if(from is null)
	{
		return default!;
	}
		var to = new Cdms.Model.Ipaffs.IpaffsInternationalTelephone ();
to.CountryCode = from.CountryCode;
            to.SubscriberNumber = from.SubscriberNumber;
            	return to;
	}
}

