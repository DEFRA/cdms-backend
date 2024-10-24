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
/// Approved Establishment details
/// </summary>
public partial class IpaffsApprovedEstablishment  //
{


    /// <summary>
    /// ID
    /// </summary
    [Attr]
    [System.ComponentModel.Description("ID")]
    public string? IpaffsId { get; set; }

	
    /// <summary>
    /// Name of approved establishment
    /// </summary
    [Attr]
    [System.ComponentModel.Description("Name of approved establishment")]
    public string? Name { get; set; }

	
    /// <summary>
    /// Country of approved establishment
    /// </summary
    [Attr]
    [System.ComponentModel.Description("Country of approved establishment")]
    public string? Country { get; set; }

	
    /// <summary>
    /// Types of approved establishment
    /// </summary
    [Attr]
    [System.ComponentModel.Description("Types of approved establishment")]
    public string[]? Types { get; set; }

	
    /// <summary>
    /// Approval number
    /// </summary
    [Attr]
    [System.ComponentModel.Description("Approval number")]
    public string? ApprovalNumber { get; set; }

	
    /// <summary>
    /// Section of approved establishment
    /// </summary
    [Attr]
    [System.ComponentModel.Description("Section of approved establishment")]
    public string? Section { get; set; }

	}


