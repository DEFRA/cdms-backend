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

public static class DetailsOnReExportMapper
{
	public static Cdms.Model.Ipaffs.DetailsOnReExport Map(Cdms.Types.Ipaffs.DetailsOnReExport from)
	{
	if(from is null)
	{
		return default!;
	}
		var to = new Cdms.Model.Ipaffs.DetailsOnReExport ();
to.Date = from?.Date;
            to.MeansOfTransportNo = from?.MeansOfTransportNo;
            to.TransportType = DetailsOnReExportTransportTypeEnumMapper.Map(from?.TransportType);
                to.Document = from?.Document;
            to.CountryOfReDispatching = from?.CountryOfReDispatching;
            to.ExitBip = from?.ExitBip;
            	return to;
	}
}

