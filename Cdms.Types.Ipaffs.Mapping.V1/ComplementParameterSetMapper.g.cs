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

public static class ComplementParameterSetMapper
{
	public static Cdms.Model.Ipaffs.ComplementParameterSet Map(Cdms.Types.Ipaffs.ComplementParameterSet from)
	{
	if(from is null)
	{
		return default!;
	}
		var to = new Cdms.Model.Ipaffs.ComplementParameterSet ();
to.UniqueComplementId = from?.UniqueComplementId;
            to.ComplementId = from?.ComplementId;
            to.SpeciesId = from?.SpeciesId;
            to.KeyDataPairs = Cdms.Types.Ipaffs.Mapping.DictionaryMapper.Map(from?.KeyDataPairs);
            to.CatchCertificates = from?.CatchCertificates?.Select(x => CatchCertificatesMapper.Map(x)).ToArray();
                to.Identifiers = from?.Identifiers?.Select(x => IdentifiersMapper.Map(x)).ToArray();
                	return to;
	}
}

