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

public static class SplitConsignmentMapper
{
	public static Cdms.Model.Ipaffs.SplitConsignment Map(Cdms.Types.Ipaffs.SplitConsignment from)
	{
	if(from is null)
	{
		return default!;
	}
		var to = new Cdms.Model.Ipaffs.SplitConsignment ();
to.ValidReferenceNumber = from?.ValidReferenceNumber;
            to.RejectedReferenceNumber = from?.RejectedReferenceNumber;
            	return to;
	}
}

