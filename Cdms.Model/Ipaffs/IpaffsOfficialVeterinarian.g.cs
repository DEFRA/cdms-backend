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
/// Official veterinarian information
/// </summary>
public partial class IpaffsOfficialVeterinarian  //
{


    /// <summary>
    /// First name of official veterinarian
    /// </summary
    [Attr]
    [System.ComponentModel.Description("First name of official veterinarian")]
    public string? FirstName { get; set; }

	
    /// <summary>
    /// Last name of official veterinarian
    /// </summary
    [Attr]
    [System.ComponentModel.Description("Last name of official veterinarian")]
    public string? LastName { get; set; }

	
    /// <summary>
    /// Email address of official veterinarian
    /// </summary
    [Attr]
    [System.ComponentModel.Description("Email address of official veterinarian")]
    public string? Email { get; set; }

	
    /// <summary>
    /// Phone number of official veterinarian
    /// </summary
    [Attr]
    [System.ComponentModel.Description("Phone number of official veterinarian")]
    public string? Phone { get; set; }

	
    /// <summary>
    /// Fax number of official veterinarian
    /// </summary
    [Attr]
    [System.ComponentModel.Description("Fax number of official veterinarian")]
    public string? Fax { get; set; }

	
    /// <summary>
    /// Date of sign
    /// </summary
    [Attr]
    [System.ComponentModel.Description("Date of sign")]
    public string? Signed { get; set; }

	}


