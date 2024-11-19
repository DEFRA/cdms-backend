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


namespace Cdms.Model.Gvms;

/// <summary>
/// 
/// </summary>
public partial class Gmr  //
{


        /// <summary>
        /// The Goods Movement Record (GMR) ID for this GMR.  Do not include when POSTing a GMR - GVMS will assign an ID.
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("The Goods Movement Record (GMR) ID for this GMR.  Do not include when POSTing a GMR - GVMS will assign an ID.")]
    public string? Id { get; set; }

	
        /// <summary>
        /// The EORI of the haulier that is responsible for the vehicle making the goods movement
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("The EORI of the haulier that is responsible for the vehicle making the goods movement")]
    public string? HaulierEori { get; set; }

	
        /// <summary>
        /// The state of the GMR
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("The state of the GMR")]
    [MongoDB.Bson.Serialization.Attributes.BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public StateEnum? State { get; set; }

	
        /// <summary>
        /// If set to true, indicates that the vehicle requires a customs inspection.  If set to false, indicates that the vehicle does not require a customs inspection.  If not set, indicates the customs inspection decision has not yet been made or is not applicable.  For outbound GMRs, this indicates that the vehicle must present at an inspection facility prior to checking-in at the port.  For Office of Transit inspections for inbound GMRs, a decision may be made to inspect subsequent to a notification that inspection is not required.  In this event a notification will be sent that changes inspectionRequired from false to true.  If this happens after leaving the port of arrival, the inspection should be carried out at the next transit office e.g. the office of destination.
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("If set to true, indicates that the vehicle requires a customs inspection.  If set to false, indicates that the vehicle does not require a customs inspection.  If not set, indicates the customs inspection decision has not yet been made or is not applicable.  For outbound GMRs, this indicates that the vehicle must present at an inspection facility prior to checking-in at the port.  For Office of Transit inspections for inbound GMRs, a decision may be made to inspect subsequent to a notification that inspection is not required.  In this event a notification will be sent that changes inspectionRequired from false to true.  If this happens after leaving the port of arrival, the inspection should be carried out at the next transit office e.g. the office of destination.")]
    public bool? InspectionRequired { get; set; }

	
        /// <summary>
        /// A list of one or more inspection types, from GVMS Reference Data, that the vehicle must have carried out, in the order specified.  This means that where there are multiple entries here, the vehicle must report for the first inspection type listed before each subsequent listed inspection.  Each entry includes a list of available locations for the inspection type.
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("A list of one or more inspection types, from GVMS Reference Data, that the vehicle must have carried out, in the order specified.  This means that where there are multiple entries here, the vehicle must report for the first inspection type listed before each subsequent listed inspection.  Each entry includes a list of available locations for the inspection type.")]
    public ReportToLocations[]? ReportToLocations { get; set; }

	
        /// <summary>
        /// The date and time that this GMR was last updated.
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("The date and time that this GMR was last updated.")]
    public DateTime? CreatedSource { get; set; }

	
        /// <summary>
        /// The direction of the movement - into or out of the UK, or between Great Britain and Northern Ireland
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("The direction of the movement - into or out of the UK, or between Great Britain and Northern Ireland")]
    [MongoDB.Bson.Serialization.Attributes.BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public DirectionEnum? Direction { get; set; }

	
        /// <summary>
        /// The type of haulier moving the goods
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("The type of haulier moving the goods")]
    [MongoDB.Bson.Serialization.Attributes.BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public HaulierTypeEnum? HaulierType { get; set; }

	
        /// <summary>
        /// Set to true if the vehicle will not be accompanying the trailer(s) during the crossing, or if the vehicle is carrying a container that will be detached and loaded for the crossing.
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Set to true if the vehicle will not be accompanying the trailer(s) during the crossing, or if the vehicle is carrying a container that will be detached and loaded for the crossing.")]
    public bool? IsUnaccompanied { get; set; }

	
        /// <summary>
        /// Vehicle registration number of the vehicle that will arrive at port.  If isUnaccompanied is set to false then vehicleRegNum must be provided to check-in.
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Vehicle registration number of the vehicle that will arrive at port.  If isUnaccompanied is set to false then vehicleRegNum must be provided to check-in.")]
    public string? VehicleRegistrationNumber { get; set; }

	
        /// <summary>
        /// 
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("")]
    public PlannedCrossing? PlannedCrossing { get; set; }

	
        /// <summary>
        /// 
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("")]
    public CheckedInCrossing? CheckedInCrossing { get; set; }

	
        /// <summary>
        /// 
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("")]
    public ActualCrossing? ActualCrossing { get; set; }

	}


