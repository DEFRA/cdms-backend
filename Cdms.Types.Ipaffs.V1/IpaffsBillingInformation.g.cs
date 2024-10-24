//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#nullable enable

using System.Text.Json.Serialization;
using System.Dynamic;


namespace Cdms.Types.Ipaffs;

/// <summary>
/// 
/// </summary>
public partial class IpaffsBillingInformation  //
{


    /// <summary>
    /// Indicates whether user has confirmed their billing information
    /// </summary
    [JsonPropertyName("isConfirmed")]
    public bool? IsConfirmed { get; set; }

	
    /// <summary>
    /// Billing email address
    /// </summary
    [JsonPropertyName("emailAddress")]
    public string? EmailAddress { get; set; }

	
    /// <summary>
    /// Billing phone number
    /// </summary
    [JsonPropertyName("phoneNumber")]
    public string? PhoneNumber { get; set; }

	
    /// <summary>
    /// Billing Contact Name
    /// </summary
    [JsonPropertyName("contactName")]
    public string? ContactName { get; set; }

	
    /// <summary>
    /// Billing postal address
    /// </summary
    [JsonPropertyName("postalAddress")]
    public IpaffsPostalAddress? PostalAddress { get; set; }

	}


