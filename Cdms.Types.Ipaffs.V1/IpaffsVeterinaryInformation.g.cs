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
/// Part 1 - Holds the information related to veterinary checks and details
/// </summary>
public partial class IpaffsVeterinaryInformation  //
{


    /// <summary>
    /// External reference of approved establishments, which relates to a downstream service
    /// </summary
    [JsonPropertyName("establishmentsOfOriginExternalReference")]
    public IpaffsExternalReference? EstablishmentsOfOriginExternalReference { get; set; }

	
    /// <summary>
    /// List of establishments which were approved by UK to issue veterinary documents
    /// </summary
    [JsonPropertyName("establishmentsOfOrigin")]
    public IpaffsApprovedEstablishment[]? EstablishmentsOfOrigins { get; set; }

	
    /// <summary>
    /// Veterinary document identification
    /// </summary
    [JsonPropertyName("veterinaryDocument")]
    public string? VeterinaryDocument { get; set; }

	
    /// <summary>
    /// Veterinary document issue date
    /// </summary
    [JsonPropertyName("veterinaryDocumentIssueDate")]
    public DateOnly? VeterinaryDocumentIssueDate { get; set; }

	
    /// <summary>
    /// Additional documents
    /// </summary
    [JsonPropertyName("accompanyingDocumentNumbers")]
    public string[]? AccompanyingDocumentNumbers { get; set; }

	
    /// <summary>
    /// Accompanying documents
    /// </summary
    [JsonPropertyName("accompanyingDocuments")]
    public IpaffsAccompanyingDocument[]? AccompanyingDocuments { get; set; }

	
    /// <summary>
    /// Catch certificate attachments
    /// </summary
    [JsonPropertyName("catchCertificateAttachments")]
    public IpaffsCatchCertificateAttachment[]? CatchCertificateAttachments { get; set; }

	
    /// <summary>
    /// Details helpful for identification
    /// </summary
    [JsonPropertyName("identificationDetails")]
    public IpaffsIdentificationDetails[]? IdentificationDetails { get; set; }

	}


