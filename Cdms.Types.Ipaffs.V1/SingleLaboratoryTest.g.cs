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
/// Information about single laboratory test
/// </summary>
public partial class SingleLaboratoryTest  //
{


        /// <summary>
        /// Commodity code for which lab test was ordered
        /// </summary>
    [JsonPropertyName("commodityCode")]
    public string? CommodityCode { get; set; }

	
        /// <summary>
        /// Species id of commodity for which lab test was ordered
        /// </summary>
    [JsonPropertyName("speciesID")]
    public int? SpeciesId { get; set; }

	
        /// <summary>
        /// TRACES ID
        /// </summary>
    [JsonPropertyName("tracesID")]
    public int? TracesId { get; set; }

	
        /// <summary>
        /// Test name
        /// </summary>
    [JsonPropertyName("testName")]
    public string? TestName { get; set; }

	
        /// <summary>
        /// Laboratory tests information details and information about laboratory
        /// </summary>
    [JsonPropertyName("applicant")]
    public Applicant? Applicant { get; set; }

	
        /// <summary>
        /// Information about results of test
        /// </summary>
    [JsonPropertyName("laboratoryTestResult")]
    public LaboratoryTestResult? LaboratoryTestResult { get; set; }

	}

