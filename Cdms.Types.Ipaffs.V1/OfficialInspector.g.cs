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
/// Official inspector details
/// </summary>
public partial class OfficialInspector  //
{


        /// <summary>
        /// First name of inspector
        /// </summary>
    [JsonPropertyName("firstName")]
    public string? FirstName { get; set; }

	
        /// <summary>
        /// Last name of inspector
        /// </summary>
    [JsonPropertyName("lastName")]
    public string? LastName { get; set; }

	
        /// <summary>
        /// Email of inspector
        /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

	
        /// <summary>
        /// Phone number of inspector
        /// </summary>
    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

	
        /// <summary>
        /// Fax number of inspector
        /// </summary>
    [JsonPropertyName("fax")]
    public string? Fax { get; set; }

	
        /// <summary>
        /// Address of inspector
        /// </summary>
    [JsonPropertyName("address")]
    public Address? Address { get; set; }

	
        /// <summary>
        /// Date of sign
        /// </summary>
    [JsonPropertyName("signed")]
    public string? Signed { get; set; }

	}

