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

public static class BillingInformationMapper
{
    public static Cdms.Model.Ipaffs.BillingInformation Map(Cdms.Types.Ipaffs.BillingInformation from)
    {
        if (from is null)
        {
            return default!;
        }

        var to = new Cdms.Model.Ipaffs.BillingInformation();
        to.IsConfirmed = from?.IsConfirmed;
        to.EmailAddress = from?.EmailAddress;
        to.PhoneNumber = from?.PhoneNumber;
        to.ContactName = from?.ContactName;
        to.PostalAddress = PostalAddressMapper.Map(from?.PostalAddress!);
        return to;
    }
}

