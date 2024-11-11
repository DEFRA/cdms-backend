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
public partial class PartOne  //
{


        /// <summary>
        /// Used to indicate what type of EU Import the notification is - Live Animals, Product Of Animal Origin or High Risk Food Not Of Animal Origin
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Used to indicate what type of EU Import the notification is - Live Animals, Product Of Animal Origin or High Risk Food Not Of Animal Origin")]
    [MongoDB.Bson.Serialization.Attributes.BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public PartOneTypeOfImpEnum? TypeOfImp { get; set; }

	
        /// <summary>
        /// The individual who has submitted the notification
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("The individual who has submitted the notification")]
    public Party? PersonResponsible { get; set; }

	
        /// <summary>
        /// Customs reference number
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Customs reference number")]
    public string? CustomsReferenceNumber { get; set; }

	
        /// <summary>
        /// (Deprecated in IMTA-11832) Does the consignment contain wood packaging?
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("(Deprecated in IMTA-11832) Does the consignment contain wood packaging?")]
    public bool? ContainsWoodPackaging { get; set; }

	
        /// <summary>
        /// Has the consignment arrived at the BCP?
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Has the consignment arrived at the BCP?")]
    public bool? ConsignmentArrived { get; set; }

	
        /// <summary>
        /// Person or Company that sends shipment
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Person or Company that sends shipment")]
    public EconomicOperator? Consignor { get; set; }

	
        /// <summary>
        /// Person or Company that sends shipment
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Person or Company that sends shipment")]
    public EconomicOperator? ConsignorTwo { get; set; }

	
        /// <summary>
        /// Person or Company that packs the shipment
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Person or Company that packs the shipment")]
    public EconomicOperator? Packer { get; set; }

	
        /// <summary>
        /// Person or Company that receives shipment
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Person or Company that receives shipment")]
    public EconomicOperator? Consignee { get; set; }

	
        /// <summary>
        /// Person or Company that is importing the consignment
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Person or Company that is importing the consignment")]
    public EconomicOperator? Importer { get; set; }

	
        /// <summary>
        /// Where the shipment is to be sent? For IMP minimum 48 hour accommodation/holding location.
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Where the shipment is to be sent? For IMP minimum 48 hour accommodation/holding location.")]
    public EconomicOperator? PlaceOfDestination { get; set; }

	
        /// <summary>
        /// A temporary place of destination for plants
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("A temporary place of destination for plants")]
    public EconomicOperator? Pod { get; set; }

	
        /// <summary>
        /// Place in which the animals or products originate
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Place in which the animals or products originate")]
    public EconomicOperator? PlaceOfOriginHarvest { get; set; }

	
        /// <summary>
        /// List of additional permanent addresses
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("List of additional permanent addresses")]
    public EconomicOperator[]? AdditionalPermanentAddresses { get; set; }

	
        /// <summary>
        /// Charity Parish Holding number
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Charity Parish Holding number")]
    public string? CphNumber { get; set; }

	
        /// <summary>
        /// Is the importer importing from a charity?
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Is the importer importing from a charity?")]
    public bool? ImportingFromCharity { get; set; }

	
        /// <summary>
        /// Is the place of destination the permanent address?
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Is the place of destination the permanent address?")]
    public bool? IsPlaceOfDestinationThePermanentAddress { get; set; }

	
        /// <summary>
        /// Is this catch certificate required?
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Is this catch certificate required?")]
    public bool? IsCatchCertificateRequired { get; set; }

	
        /// <summary>
        /// Is GVMS route?
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Is GVMS route?")]
    public bool? IsGvmsRoute { get; set; }

	
        /// <summary>
        /// Purpose of consignment details
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Purpose of consignment details")]
    public Purpose? Purpose { get; set; }

	
        /// <summary>
        /// Either a Border-Inspection-Post or Designated-Point-Of-Entry, e.g. GBFXT1
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Either a Border-Inspection-Post or Designated-Point-Of-Entry, e.g. GBFXT1")]
    public string? PointOfEntry { get; set; }

	
        /// <summary>
        /// A control point at the point of entry
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("A control point at the point of entry")]
    public string? PointOfEntryControlPoint { get; set; }

	
        /// <summary>
        /// How consignment is transported after BIP
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("How consignment is transported after BIP")]
    public MeansOfTransport? MeansOfTransport { get; set; }

	
        /// <summary>
        /// Transporter of consignment details
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Transporter of consignment details")]
    public EconomicOperator? Transporter { get; set; }

	
        /// <summary>
        /// Are transporter details required for this consignment
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Are transporter details required for this consignment")]
    public bool? TransporterDetailsRequired { get; set; }

	
        /// <summary>
        /// Transport to BIP
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Transport to BIP")]
    public MeansOfTransport? MeansOfTransportFromEntryPoint { get; set; }

	
        /// <summary>
        /// Estimated journey time in minutes to point of entry
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Estimated journey time in minutes to point of entry")]
    public double? EstimatedJourneyTimeInMinutes { get; set; }

	
        /// <summary>
        /// (Deprecated in IMTA-12139) Person who is responsible for transport
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("(Deprecated in IMTA-12139) Person who is responsible for transport")]
    public string? ResponsibleForTransport { get; set; }

	
        /// <summary>
        /// Part 1 - Holds the information related to veterinary checks and details
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Part 1 - Holds the information related to veterinary checks and details")]
    public VeterinaryInformation? VeterinaryInformation { get; set; }

	
        /// <summary>
        /// Reference number added by the importer
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Reference number added by the importer")]
    public string? ImporterLocalReferenceNumber { get; set; }

	
        /// <summary>
        /// Contains countries and transfer points that consignment is going through
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Contains countries and transfer points that consignment is going through")]
    public Route? Route { get; set; }

	
        /// <summary>
        /// Array that contains pair of seal number and container number
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Array that contains pair of seal number and container number")]
    public SealContainer[]? SealsContainers { get; set; }

	
        /// <summary>
        /// Date and time when the notification was submitted
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Date and time when the notification was submitted")]
    public DateTime? SubmittedOn { get; set; }

	
        /// <summary>
        /// Information about user who submitted notification
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Information about user who submitted notification")]
    public UserInformation? SubmittedBy { get; set; }

	
        /// <summary>
        /// Validation messages for whole notification
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Validation messages for whole notification")]
    public ValidationMessageCode[]? ConsignmentValidations { get; set; }

	
        /// <summary>
        /// Was complex commodity selected. Indicating if importer provided commodity code.
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Was complex commodity selected. Indicating if importer provided commodity code.")]
    public bool? ComplexCommoditySelected { get; set; }

	
        /// <summary>
        /// Entry port for EU Import notification.
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Entry port for EU Import notification.")]
    public string? PortOfEntry { get; set; }

	
        /// <summary>
        /// Exit Port for EU Import Notification.
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Exit Port for EU Import Notification.")]
    public string? PortOfExit { get; set; }

	
        /// <summary>
        /// Date of Port Exit for EU Import Notification.
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Date of Port Exit for EU Import Notification.")]
    public DateTime? ExitedPortOfOn { get; set; }

	
        /// <summary>
        /// Person to be contacted if there is an issue with the consignment
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Person to be contacted if there is an issue with the consignment")]
    public ContactDetails? ContactDetails { get; set; }

	
        /// <summary>
        /// List of nominated contacts to receive text and email notifications
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("List of nominated contacts to receive text and email notifications")]
    public NominatedContact[]? NominatedContacts { get; set; }

	
        /// <summary>
        /// Original estimated date time of arrival
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Original estimated date time of arrival")]
    public DateTime? OriginalEstimatedOn { get; set; }

	
        /// <summary>
        /// 
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("")]
    public BillingInformation? BillingInformation { get; set; }

	
        /// <summary>
        /// Indicates whether CUC applies to the notification
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Indicates whether CUC applies to the notification")]
    public bool? IsChargeable { get; set; }

	
        /// <summary>
        /// Indicates whether CUC previously applied to the notification
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("Indicates whether CUC previously applied to the notification")]
    public bool? WasChargeable { get; set; }

	
        /// <summary>
        /// 
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("")]
    public CommonUserCharge? CommonUserCharge { get; set; }

	
        /// <summary>
        /// When the NCTS MRN will be added for the Common Transit Convention (CTC)
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("When the NCTS MRN will be added for the Common Transit Convention (CTC)")]
    [MongoDB.Bson.Serialization.Attributes.BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public PartOneProvideCtcMrnEnum? ProvideCtcMrn { get; set; }

	
        /// <summary>
        /// DateTime
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("DateTime")]
    public DateTime? ArrivedOn { get; set; }

	
        /// <summary>
        /// DateTime
        /// </summary>
    [Attr]
    [System.ComponentModel.Description("DateTime")]
    public DateTime? DepartedOn { get; set; }

	}


