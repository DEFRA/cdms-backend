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
/// Tests results corresponding to LaboratoryTests
/// </summary>
public partial class LaboratoryTestResult  //
{


        /// <summary>
        /// When sample was used
        /// </summary>
    [JsonPropertyName("sampleUseByDate")]
    public string? SampleUseByDate { get; set; }

	
        /// <summary>
        /// When it was released
        /// </summary>
    [JsonPropertyName("releasedDate")]
    public DateTime? ReleasedDate { get; set; }

	
        /// <summary>
        /// Laboratory test method
        /// </summary>
    [JsonPropertyName("laboratoryTestMethod")]
    public string? LaboratoryTestMethod { get; set; }

	
        /// <summary>
        /// Result of test
        /// </summary>
    [JsonPropertyName("results")]
    public string? Results { get; set; }

	
        /// <summary>
        /// Conclusion of laboratory test
        /// </summary>
    [JsonPropertyName("conclusion")]
    public LaboratoryTestResultConclusionEnum? Conclusion { get; set; }

	
        /// <summary>
        /// Date of lab test created in IPAFFS
        /// </summary>
    [JsonPropertyName("labTestCreatedDate")]
    public DateTime? LabTestCreatedDate { get; set; }

	}

