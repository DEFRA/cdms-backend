using Cdms.Common.Extensions;
using Cdms.Types.Ipaffs;
using Microsoft.Extensions.Logging;
using TestDataGenerator.Helpers;

namespace TestDataGenerator.Scenarios;

public class CrNoMatchScenarioGenerator(ILogger<CrNoMatchScenarioGenerator> logger) : ScenarioGenerator
{
    public override GeneratorResult Generate(int scenario, int item, DateTime entryDate, ScenarioConfig config)
    {
        var clearanceRequest = GetClearanceRequestBuilder("cr-one-item")
            .WithEntryDate(entryDate)
            .WithArrivalDateTimeOffset(DateTime.Today.ToDate(), DateTime.Now.ToTime())
            .WithReferenceNumber(DataHelpers.GenerateReferenceNumber(ImportNotificationTypeEnum.Cveda, scenario, entryDate, item))
            .WithRandomItems(10, 100)
            .ValidateAndBuild();

        logger.LogInformation("Created {EntryReference}", clearanceRequest.Header!.EntryReference);

        return new GeneratorResult { ClearanceRequests = [clearanceRequest], ImportNotifications = [] };
    }
}