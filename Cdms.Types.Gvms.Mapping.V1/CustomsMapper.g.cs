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


namespace Cdms.Types.Gmr.Mapping;

public static class CustomsMapper
{
	public static Cdms.Model.VehicleMovement.Customs Map(Cdms.Types.Gmr.Customs from)
	{
	if(from is null)
	{
		return default!;
	}
		var to = new Cdms.Model.VehicleMovement.Customs ();
to.Id = from.Id;
            	return to;
	}
}
