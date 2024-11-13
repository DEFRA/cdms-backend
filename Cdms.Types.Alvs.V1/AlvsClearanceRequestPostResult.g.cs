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


namespace Cdms.Types.Alvs;

/// <summary>
/// Message returned by the server as answer to the ALVSClearanceRequest.
/// </summary>
public partial class AlvsClearanceRequestPostResult  //
{


    /// <summary>
    /// 
    /// </summary
    [JsonPropertyName("xmlSchemaVersion")]
    public string? XmlSchemaVersion { get; set; }

	
    /// <summary>
    /// 
    /// </summary
    [JsonPropertyName("sendingDate")]
    public DateTime? SendingDate { get; set; }

	
    /// <summary>
    /// 
    /// </summary
    [JsonPropertyName("operationCode")]
    public int? OperationCode { get; set; }

	
    /// <summary>
    /// 
    /// </summary
    [JsonPropertyName("requestIdentifier")]
    public string? RequestIdentifier { get; set; }

	}


