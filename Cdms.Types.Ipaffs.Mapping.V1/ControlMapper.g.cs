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

public static class ControlMapper
{
	public static Cdms.Model.Ipaffs.Control Map(Cdms.Types.Ipaffs.Control from)
	{
	if(from is null)
	{
		return default!;
	}
		var to = new Cdms.Model.Ipaffs.Control ();
to.FeedbackInformation = FeedbackInformationMapper.Map(from?.FeedbackInformation);
                to.DetailsOnReExport = DetailsOnReExportMapper.Map(from?.DetailsOnReExport);
                to.OfficialInspector = OfficialInspectorMapper.Map(from?.OfficialInspector);
                to.ConsignmentLeave = ControlConsignmentLeaveEnumMapper.Map(from?.ConsignmentLeave);
                	return to;
	}
}
