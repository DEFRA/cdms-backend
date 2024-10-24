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
/// Billing postal address
/// </summary>
public partial class IpaffsPostalAddress  //
{


    /// <summary>
    /// 1st line of address
    /// </summary
    [JsonPropertyName("addressLine1")]
    public string? AddressLine1 { get; set; }

	
    /// <summary>
    /// 2nd line of address
    /// </summary
    [JsonPropertyName("addressLine2")]
    public string? AddressLine2 { get; set; }

	
    /// <summary>
    /// 3rd line of address
    /// </summary
    [JsonPropertyName("addressLine3")]
    public string? AddressLine3 { get; set; }

	
    /// <summary>
    /// 4th line of address
    /// </summary
    [JsonPropertyName("addressLine4")]
    public string? AddressLine4 { get; set; }

	
    /// <summary>
    /// 3rd line of address
    /// </summary
    [JsonPropertyName("county")]
    public string? County { get; set; }

	
    /// <summary>
    /// City or town name
    /// </summary
    [JsonPropertyName("cityOrTown")]
    public string? CityOrTown { get; set; }

	
    /// <summary>
    /// Post code
    /// </summary
    [JsonPropertyName("postalCode")]
    public string? PostalCode { get; set; }

	}


