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

public static class IpaffsKeyDataPairMapper
{
	public static Cdms.Model.Ipaffs.IpaffsKeyDataPair Map(Cdms.Types.Ipaffs.IpaffsKeyDataPair from)
	{
	if(from is null)
	{
		return default!;
	}
		var to = new Cdms.Model.Ipaffs.IpaffsKeyDataPair ();
to.Key = from.Key;
            to.Data = from.Data;
            	return to;
	}
}

