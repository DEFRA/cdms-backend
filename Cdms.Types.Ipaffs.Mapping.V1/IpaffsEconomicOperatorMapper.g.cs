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

public static class IpaffsEconomicOperatorMapper
{
	public static Cdms.Model.Ipaffs.IpaffsEconomicOperator Map(Cdms.Types.Ipaffs.IpaffsEconomicOperator from)
	{
	if(from is null)
	{
		return default!;
	}
		var to = new Cdms.Model.Ipaffs.IpaffsEconomicOperator ();
to.IpaffsId = from.IpaffsId;
            to.IpaffsType = IpaffsEconomicOperatorTypeEnumMapper.Map(from?.IpaffsType);
                to.Status = IpaffsEconomicOperatorStatusEnumMapper.Map(from?.Status);
                to.CompanyName = from.CompanyName;
            to.IndividualName = from.IndividualName;
            to.Address = IpaffsAddressMapper.Map(from?.Address);
                to.ApprovalNumber = from.ApprovalNumber;
            to.OtherIdentifier = from.OtherIdentifier;
            to.TracesId = from.TracesId;
            	return to;
	}
}

