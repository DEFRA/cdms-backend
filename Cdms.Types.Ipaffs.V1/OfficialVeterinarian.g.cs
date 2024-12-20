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
/// Official veterinarian information
/// </summary>
public partial class OfficialVeterinarian  //
{


        /// <summary>
        /// First name of official veterinarian
        /// </summary>
    [JsonPropertyName("firstName")]
    [Cdms.SensitiveData.SensitiveData]
    public string? FirstName { get; set; }

	
        /// <summary>
        /// Last name of official veterinarian
        /// </summary>
    [JsonPropertyName("lastName")]
    [Cdms.SensitiveData.SensitiveData]
    public string? LastName { get; set; }

	
        /// <summary>
        /// Email address of official veterinarian
        /// </summary>
    [JsonPropertyName("email")]
    [Cdms.SensitiveData.SensitiveData]
    public string? Email { get; set; }

	
        /// <summary>
        /// Phone number of official veterinarian
        /// </summary>
    [JsonPropertyName("phone")]
    [Cdms.SensitiveData.SensitiveData]
    public string? Phone { get; set; }

	
        /// <summary>
        /// Fax number of official veterinarian
        /// </summary>
    [JsonPropertyName("fax")]
    [Cdms.SensitiveData.SensitiveData]
    public string? Fax { get; set; }

	
        /// <summary>
        /// Date of sign
        /// </summary>
    [JsonPropertyName("signed")]
    public string? Signed { get; set; }

	}


