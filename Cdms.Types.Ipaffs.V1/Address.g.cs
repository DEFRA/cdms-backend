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
/// Inspector Address
/// </summary>
public partial class Address  //
{


        /// <summary>
        /// Street
        /// </summary>
    [JsonPropertyName("street")]
    [Cdms.SensitiveData.SensitiveData]
    public string? Street { get; set; }

	
        /// <summary>
        /// City
        /// </summary>
    [JsonPropertyName("city")]
    [Cdms.SensitiveData.SensitiveData]
    public string? City { get; set; }

	
        /// <summary>
        /// Country
        /// </summary>
    [JsonPropertyName("country")]
    public string? Country { get; set; }

	
        /// <summary>
        /// Postal Code
        /// </summary>
    [JsonPropertyName("postalCode")]
    [Cdms.SensitiveData.SensitiveData]
    public string? PostalCode { get; set; }

	
        /// <summary>
        /// 1st line of address
        /// </summary>
    [JsonPropertyName("addressLine1")]
    [Cdms.SensitiveData.SensitiveData]
    public string? AddressLine1 { get; set; }

	
        /// <summary>
        /// 2nd line of address
        /// </summary>
    [JsonPropertyName("addressLine2")]
    [Cdms.SensitiveData.SensitiveData]
    public string? AddressLine2 { get; set; }

	
        /// <summary>
        /// 3rd line of address
        /// </summary>
    [JsonPropertyName("addressLine3")]
    [Cdms.SensitiveData.SensitiveData]
    public string? AddressLine3 { get; set; }

	
        /// <summary>
        /// Post / zip code
        /// </summary>
    [JsonPropertyName("postalZipCode")]
    [Cdms.SensitiveData.SensitiveData]
    public string? PostalZipCode { get; set; }

	
        /// <summary>
        /// country 2-digits ISO code
        /// </summary>
    [JsonPropertyName("countryISOCode")]
    public string? CountryIsoCode { get; set; }

	
        /// <summary>
        /// Email address
        /// </summary>
    [JsonPropertyName("email")]
    [Cdms.SensitiveData.SensitiveData]
    public string? Email { get; set; }

	
        /// <summary>
        /// UK phone number
        /// </summary>
    [JsonPropertyName("ukTelephone")]
    [Cdms.SensitiveData.SensitiveData]
    public string? UkTelephone { get; set; }

	
        /// <summary>
        /// Telephone number
        /// </summary>
    [JsonPropertyName("telephone")]
    [Cdms.SensitiveData.SensitiveData]
    public string? Telephone { get; set; }

	
        /// <summary>
        /// International phone number
        /// </summary>
    [JsonPropertyName("internationalTelephone")]
    public InternationalTelephone? InternationalTelephone { get; set; }

	}


