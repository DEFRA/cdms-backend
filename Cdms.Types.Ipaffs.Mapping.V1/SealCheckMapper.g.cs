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

public static class SealCheckMapper
{
	public static Cdms.Model.Ipaffs.SealCheck Map(Cdms.Types.Ipaffs.SealCheck from)
	{
	if(from is null)
	{
		return default!;
	}
		var to = new Cdms.Model.Ipaffs.SealCheck ();
to.Satisfactory = from.Satisfactory;
            to.Reason = from.Reason;
            to.OfficialInspector = OfficialInspectorMapper.Map(from?.OfficialInspector);
                to.CheckedOn = from.DateTimeOfCheck;
            	return to;
	}
}
