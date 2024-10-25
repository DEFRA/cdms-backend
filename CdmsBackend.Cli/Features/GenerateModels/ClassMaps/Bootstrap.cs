using CdmsBackend.Cli.Features.GenerateModels.DescriptorModel;
using CdmsBackend.Cli.Features.GenerateModels.GenerateIpaffsModel.Builders;

namespace CdmsBackend.Cli.Features.GenerateModels.ClassMaps;

static class Bootstrap
{
    public static void GeneratorClassMaps()
    {
        RegisterAlvsClassMaps();
        RegisterIpaffsClassMaps();
        RegisterIpaffsEnumMaps();

        RegisterVehicleMovementsClassMaps();
    }

    public static void RegisterAlvsClassMaps()
    {
        GeneratorClassMap.RegisterClassMap("Header", map =>
        {
            map.MapProperty("ArrivalDateTime").IsDateTime().SetInternalName("ArrivedAt");
            map.MapProperty("MasterUCR").SetName("MasterUcr");
            map.MapProperty("SubmitterTURN").SetName("SubmitterTurn");
        });

        GeneratorClassMap.RegisterClassMap("ServiceHeader",
            map => { map.MapProperty("ServiceCallTimestamp").IsDateTime().SetInternalName("ServiceCalled"); });

        GeneratorClassMap.RegisterClassMap("ALVSClearanceRequest",
            map => { map.SetClassName("AlvsClearanceRequest"); });

        GeneratorClassMap.RegisterClassMap("ALVSClearanceRequestPost", map =>
        {
            map.SetClassName("AlvsClearanceRequestPost");
            map.MapProperty("AlvsClearanceRequest").SetType("AlvsClearanceRequest");
        });

        GeneratorClassMap.RegisterClassMap("ALVSClearanceRequestPostResult", map =>
        {
            map.SetClassName("AlvsClearanceRequestPostResult")
                .NoInternalClass();
        });
    }

    public static void RegisterIpaffsEnumMaps()
    {
        GeneratorEnumMap.RegisterEnumMap("ImportNotificationStatusEnum",
            map => { map.RemoveEnumValue("SUBMITTED,IN_PROGRESS,MODIFY"); });

        GeneratorEnumMap.RegisterEnumMap("purposeGroup",
            map => { map.AddEnumValue("For Import Non-Internal Market"); });
    }

    public static void RegisterIpaffsClassMaps()
    {
        GeneratorClassMap.RegisterClassMap("Decision",
            map =>
            {
                map.MapProperty("decision").SetName("DecisionEnum");
                map.MapProperty("notAcceptableActionByDate").IsDate();
            });

        GeneratorClassMap.RegisterClassMap("ImportNotification", map =>
        {
            map.MapProperty("Id").SetName("IpaffsId");
            map.MapProperty("Type").SetName("ImportNotificationType");
        });

        GeneratorClassMap.RegisterClassMap("Purpose", map =>
        {
            map.MapProperty("estimatedArrivalTimeAtPortOfExit").IsTime()
                .SetInternalName("estimatedArrivedAtPortOfExit");
            //map.MapProperty("estimatedArrivalDateAtPortOfExit").IsDate()
            //    .SetInternalName("estimatedArrivedAtPortOfExit").SetType(); 
            map.MapProperty("exitDate").IsDate();
        });

        GeneratorClassMap.RegisterClassMap("AccompanyingDocument", map =>
        {
            map.MapProperty("documentIssueDate").IsDateTime();
            map.MapProperty("documentIssueDate").IsDateTime();
        });

        GeneratorClassMap.RegisterClassMap("VeterinaryInformation",
            map => { map.MapProperty("veterinaryDocumentIssueDate").IsDate(); });

        GeneratorClassMap.RegisterClassMap("InspectionOverride",
            map => { map.MapProperty("overriddenOn").IsDateTime(); });

        GeneratorClassMap.RegisterClassMap("SealCheck", map => { map.MapProperty("dateTimeOfCheck").IsDateTime(); });

        GeneratorClassMap.RegisterClassMap("LaboratoryTests", map => { map.MapProperty("testDate").IsDateTime(); });

        GeneratorClassMap.RegisterClassMap("LaboratoryTestResult", map =>
        {
            map.MapProperty("releasedDate").IsDateTime();
            map.MapProperty("labTestCreatedDate").IsDateTime();
        });

        GeneratorClassMap.RegisterClassMap("DetailsOnReExport", map =>
        {
            map.MapProperty("date").IsDateTime();
            map.MapProperty("exitBIP").SetName("exitBip");
        });

        GeneratorClassMap.RegisterClassMap("CatchCertificateDetails",
            map => { map.MapProperty("dateOfIssue").IsDateTime(); });

        GeneratorClassMap.RegisterClassMap("JourneyRiskCategorisationResult",
            map => { map.MapProperty("riskLevelDateTime").IsDateTime(); });


        GeneratorClassMap.RegisterClassMap("RiskAssessmentResult",
            map => { map.MapProperty("assessmentDateTime").IsDateTime(); });

        GeneratorClassMap.RegisterClassMap("Notification", map =>
        {
            map.MapProperty("isGMRMatched").SetName("isGmrMatched");
            map.MapProperty("riskDecisionLockingTime").IsDateTime();
            map.MapProperty("decisionDate").IsDateTime();
            map.MapProperty("lastUpdated").IsDateTime();
            map.MapProperty("referenceNumber").SetBsonIgnore();
        });

        GeneratorClassMap.RegisterClassMap("CommodityComplement", map =>
        {
            map.AddProperty(new PropertyDescriptor("additionalData", "additionalData", "IDictionary<string, object>",
                "", false, false,
                IpaffsDescriptorBuilder.ClassNamePrefix));

            map.AddProperty(new PropertyDescriptor("riskAssesment", "riskAssesment", "CommodityRiskResult", "",
                true, false,
                IpaffsDescriptorBuilder.ClassNamePrefix));

            map.AddProperty(new PropertyDescriptor("checks", "checks", "InspectionCheck", "", true, true,
                IpaffsDescriptorBuilder.ClassNamePrefix));
        });

        GeneratorClassMap.RegisterClassMap("Commodities", map =>
        {
            map.MapProperty("complementParameterSet").SetBsonIgnore();
            map.MapProperty("commodityComplement").SetBsonIgnore();
        });


        GeneratorClassMap.RegisterClassMap("PartOne", map =>
        {
            map.MapProperty("commodities").ExcludeFromInternal();
            map.MapProperty("originalEstimatedDateTime").IsDateTime();
            map.MapProperty("submissionDate").IsDateTime();
            map.MapProperty("isGVMSRoute").SetName("isGvmsRoute");
            map.MapProperty("arrivalDate").IsDate();
            map.MapProperty("arrivalTime").IsTime();
            map.MapProperty("departureDate").IsDate();
            map.MapProperty("departureTime").IsTime();
            map.MapProperty("portOfExitDate").IsDateTime();
        });

        GeneratorClassMap.RegisterClassMap("Applicant", map =>
        {
            map.MapProperty("sampleDate").IsDate();
            map.MapProperty("sampleTime").IsTime();
        });

        GeneratorClassMap.RegisterClassMap("PartTwo", map =>
        {
            map.MapProperty("commodityChecks").ExcludeFromInternal();
            map.MapProperty("autoClearedDateTime").IsDateTime();
            map.MapProperty("checkDate").IsDate();
        });

        GeneratorClassMap.RegisterClassMap("PartThree", map => { map.MapProperty("destructionDate").IsDate(); });


        GeneratorClassMap.RegisterClassMap("ComplementParameterSet", map =>
        {
            map.MapProperty("KeyDataPair")
                .SetType("IDictionary<string, object>")
                .AddAttribute("[JsonConverter(typeof(KeyDataPairsToDictionaryStringObjectJsonConverter))]",
                    Model.Source)
                .SetMapper("Cdms.Types.Ipaffs.Mapping.DictionaryMapper");
        });


        GeneratorClassMap.RegisterClassMap("EconomicOperator", map =>
        {
            map.MapProperty("individualName").IsSensitive();
            map.MapProperty("companyName").IsSensitive();
        });

        GeneratorClassMap.RegisterClassMap("Address", map =>
        {
            map.MapProperty("Street").IsSensitive();
            map.MapProperty("City").IsSensitive();
            map.MapProperty("postalCode").IsSensitive();
            map.MapProperty("addressLine1").IsSensitive();
            map.MapProperty("addressLine2").IsSensitive();
            map.MapProperty("addressLine3").IsSensitive();
            map.MapProperty("postalZipCode").IsSensitive();
            map.MapProperty("email").IsSensitive();
            map.MapProperty("ukTelephone").IsSensitive();
            map.MapProperty("telephone").IsSensitive();
            map.MapProperty("countryISOCode").SetName("countryIsoCode");
        });


        GeneratorClassMap.RegisterClassMap("Party", map =>
        {
            map.MapProperty("email").IsSensitive();
            map.MapProperty("fax").IsSensitive();
            map.MapProperty("phone").IsSensitive();
            map.MapProperty("city").IsSensitive();
            map.MapProperty("postCode").IsSensitive();
            map.MapProperty("Address").IsSensitive();
            map.MapProperty("companyName").IsSensitive();
            map.MapProperty("name").IsSensitive();
        });

        GeneratorClassMap.RegisterClassMap("UserInformation", map => { map.MapProperty("displayName").IsSensitive(); });

        GeneratorClassMap.RegisterClassMap("OfficialVeterinarian", map =>
        {
            map.MapProperty("firstName").IsSensitive();
            map.MapProperty("lastName").IsSensitive();
            map.MapProperty("email").IsSensitive();
            map.MapProperty("phone").IsSensitive();
            map.MapProperty("fax").IsSensitive();
        });
    }


    public static void RegisterVehicleMovementsClassMaps()
    {
        GeneratorClassMap.RegisterClassMap("gmrs", map =>
        {
            map.SetClassName("Gmr");
            map.MapProperty("haulierEORI").SetName("haulierEori");
            map.MapProperty("vehicleRegNum").SetName("vehicleRegNumber");
            map.MapProperty("updatedDateTime").SetName("lastUpdated").IsDateTime();
        });

        GeneratorClassMap.RegisterClassMap("SearchGmrsForDeclarationIdsResponse",
            map => { map.MapProperty("Gmrs").SetType("Gmr[]"); });

        GeneratorClassMap.RegisterClassMap("SearchGmrsForVRNsresponse",
            map => { map.MapProperty("Gmrs").SetType("Gmr[]"); });

        GeneratorClassMap.RegisterClassMap("SearchGmrsResponse", map => { map.MapProperty("Gmrs").SetType("Gmr[]"); });


        GeneratorClassMap.RegisterClassMap("PlannedCrossing",
            map => { map.MapProperty("localDateTimeOfDeparture").IsDateTime(); });

        GeneratorClassMap.RegisterClassMap("ActualCrossing",
            map => { map.MapProperty("localDateTimeOfArrival").IsDateTime(); });

        GeneratorClassMap.RegisterClassMap("CheckedInCrossing",
            map => { map.MapProperty("localDateTimeOfArrival").IsDateTime(); });
    }
}