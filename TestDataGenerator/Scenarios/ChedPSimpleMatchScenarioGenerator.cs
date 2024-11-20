using Cdms.Types.Ipaffs;
using Microsoft.Extensions.Logging;

namespace TestDataGenerator.Scenarios;

internal class ChedPSimpleMatchScenarioGenerator(ILogger<ChedPSimpleMatchScenarioGenerator> logger) : ScenarioGenerator
{
    internal override GeneratorResult Generate(int item, DateTime entryDate)
    {
        var notification = GetNotificationBuilder("chedp-one-commodity")
            .WithEntryDate(entryDate)
            .WithReferenceNumber(ImportNotificationTypeEnum.Cvedp, item)
            .ValidateAndBuild();
        
        logger.LogInformation("Created {@Notification}, {NotificationReferenceNumber}", notification, notification.ReferenceNumber);
        
        var clearanceRequest = GetClearanceRequestBuilder("cr-one-item")
            .WithEntryDate(entryDate)
            .WithReferenceNumber(notification.ReferenceNumber!)
            .ValidateAndBuild();
        
        logger.LogInformation("Created {@ClearanceRequest}, {EntryReference}", clearanceRequest, clearanceRequest.Header!.EntryReference);

        return new GeneratorResult
        {
            ClearanceRequests = [clearanceRequest], ImportNotifications = [notification]
        };
    }
}