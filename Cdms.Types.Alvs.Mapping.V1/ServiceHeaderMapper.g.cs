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


namespace Cdms.Types.Alvs.Mapping;

public static class ServiceHeaderMapper
{
	public static Cdms.Model.Alvs.ServiceHeader Map(Cdms.Types.Alvs.ServiceHeader from)
	{
	if(from is null)
	{
		return default!;
	}
		var to = new Cdms.Model.Alvs.ServiceHeader ();
to.SourceSystem = from.SourceSystem;
            to.DestinationSystem = from.DestinationSystem;
            to.CorrelationId = from.CorrelationId;
            to.ServiceCalled = from.ServiceCallTimestamp;
            	return to;
	}
}

