using System.Text.Json;
using Cdms.Types.Alvs;
using Cdms.Types.Ipaffs;
using Microsoft.Extensions.Logging;

namespace TestDataGenerator.Scenarios;

internal class ChedASimpleMatchScenarioGenerator(ILogger<ChedASimpleMatchScenarioGenerator> logger) : ScenarioGenerator()
{
    internal override GeneratorResult Generate(int scenario, int item, DateTime entryDate)
    {
        // TODO : get a good 'pair' of notification and cr as the source templates
        
        var notification = GetNotificationBuilder("cheda-one-commodity")
            .WithEntryDate(entryDate)
            .WithReferenceNumber(ImportNotificationTypeEnum.Cveda, scenario, entryDate, item)
            .ValidateAndBuild()!;

        logger.LogInformation("Created {Notification}, {Id}", notification, notification.ReferenceNumber);

        var clearanceRequest = GetClearanceRequestBuilder("cr-one-item")
            .WithEntryDate(entryDate)
            .WithReferenceNumber(notification.ReferenceNumber!)
            .ValidateAndBuild();

        logger.LogInformation("Created {ClearanceRequest}, {Id}", clearanceRequest, clearanceRequest.Header!.EntryReference);

        return new GeneratorResult()
        {
            ClearanceRequests = new[] { clearanceRequest }, ImportNotifications = new[] { notification }
        };
    }
}