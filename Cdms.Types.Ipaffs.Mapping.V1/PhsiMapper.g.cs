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

public static class PhsiMapper
{
	public static Cdms.Model.Ipaffs.Phsi Map(Cdms.Types.Ipaffs.Phsi from)
	{
	if(from is null)
	{
		return default!;
	}
		var to = new Cdms.Model.Ipaffs.Phsi ();
to.DocumentCheck = from.DocumentCheck;
            to.IdentityCheck = from.IdentityCheck;
            to.PhysicalCheck = from.PhysicalCheck;
            	return to;
	}
}
