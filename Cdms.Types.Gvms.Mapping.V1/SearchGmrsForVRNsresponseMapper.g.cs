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


namespace Cdms.Types.Gvms.Mapping;

public static class SearchGmrsForVRNsresponseMapper
{
	public static Cdms.Model.Gvms.SearchGmrsForVRNsresponse Map(Cdms.Types.Gvms.SearchGmrsForVRNsresponse from)
	{
	if(from is null)
	{
		return default!;
	}
		var to = new Cdms.Model.Gvms.SearchGmrsForVRNsresponse ();
to.GmrsByVRNs = from?.GmrsByVrns?.Select(x => GmrsByVrnMapper.Map(x)).ToArray();
                to.Gmrs = from?.Gmrs?.Select(x => GmrMapper.Map(x)).ToArray();
                	return to;
	}
}

