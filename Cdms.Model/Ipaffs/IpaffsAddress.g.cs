//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#nullable enable

using JsonApiDotNetCore.Resources.Annotations;
using System.Text.Json.Serialization;
using System.Dynamic;


namespace Cdms.Model.Ipaffs;

/// <summary>
/// Inspector Address
/// </summary>
public partial class IpaffsAddress  //
{


    /// <summary>
    /// Street
    /// </summary
    [Attr]
    [System.ComponentModel.Description("Street")]
    public string? Street { get; set; }

	
    /// <summary>
    /// City
    /// </summary
    [Attr]
    [System.ComponentModel.Description("City")]
    public string? City { get; set; }

	
    /// <summary>
    /// Country
    /// </summary
    [Attr]
    [System.ComponentModel.Description("Country")]
    public string? Country { get; set; }

	
    /// <summary>
    /// Postal Code
    /// </summary
    [Attr]
    [System.ComponentModel.Description("Postal Code")]
    public string? PostalCode { get; set; }

	
    /// <summary>
    /// 1st line of address
    /// </summary
    [Attr]
    [System.ComponentModel.Description("1st line of address")]
    public string? AddressLine1 { get; set; }

	
    /// <summary>
    /// 2nd line of address
    /// </summary
    [Attr]
    [System.ComponentModel.Description("2nd line of address")]
    public string? AddressLine2 { get; set; }

	
    /// <summary>
    /// 3rd line of address
    /// </summary
    [Attr]
    [System.ComponentModel.Description("3rd line of address")]
    public string? AddressLine3 { get; set; }

	
    /// <summary>
    /// Post / zip code
    /// </summary
    [Attr]
    [System.ComponentModel.Description("Post / zip code")]
    public string? PostalZipCode { get; set; }

	
    /// <summary>
    /// country 2-digits ISO code
    /// </summary
    [Attr]
    [System.ComponentModel.Description("country 2-digits ISO code")]
    public string? CountryIsoCode { get; set; }

	
    /// <summary>
    /// Email address
    /// </summary
    [Attr]
    [System.ComponentModel.Description("Email address")]
    public string? Email { get; set; }

	
    /// <summary>
    /// UK phone number
    /// </summary
    [Attr]
    [System.ComponentModel.Description("UK phone number")]
    public string? UkTelephone { get; set; }

	
    /// <summary>
    /// Telephone number
    /// </summary
    [Attr]
    [System.ComponentModel.Description("Telephone number")]
    public string? Telephone { get; set; }

	
    /// <summary>
    /// International phone number
    /// </summary
    [Attr]
    [System.ComponentModel.Description("International phone number")]
    public IpaffsInternationalTelephone? InternationalTelephone { get; set; }

	}


