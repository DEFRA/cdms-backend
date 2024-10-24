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
/// 
/// </summary>
public partial class IpaffsInspectionCheck  //
{


    /// <summary>
    /// Type of check
    /// </summary
    [Attr]
    [System.ComponentModel.Description("Type of check")]
    [MongoDB.Bson.Serialization.Attributes.BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public IpaffsInspectionCheckTypeEnum? IpaffsType { get; set; }

	
    /// <summary>
    /// Status of the check
    /// </summary
    [Attr]
    [System.ComponentModel.Description("Status of the check")]
    [MongoDB.Bson.Serialization.Attributes.BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public IpaffsInspectionCheckStatusEnum? Status { get; set; }

	
    /// <summary>
    /// Reason for the status if applicable
    /// </summary
    [Attr]
    [System.ComponentModel.Description("Reason for the status if applicable")]
    public string? Reason { get; set; }

	
    /// <summary>
    /// Other reason text when selected reason is &#x27;Other&#x27;
    /// </summary
    [Attr]
    [System.ComponentModel.Description("Other reason text when selected reason is 'Other'")]
    public string? OtherReason { get; set; }

	
    /// <summary>
    /// Has commodity been selected for checks?
    /// </summary
    [Attr]
    [System.ComponentModel.Description("Has commodity been selected for checks?")]
    public bool? IsSelectedForChecks { get; set; }

	
    /// <summary>
    /// Has commodity completed this type of check
    /// </summary
    [Attr]
    [System.ComponentModel.Description("Has commodity completed this type of check")]
    public bool? HasChecksComplete { get; set; }

	}


