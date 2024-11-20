using Cdms.Types.Ipaffs;
using Microsoft.Extensions.Logging;

namespace TestDataGenerator.Scenarios;

internal class ChedPSimpleMatchScenarioGenerator(ILogger<ChedPSimpleMatchScenarioGenerator> logger) : ScenarioGenerator
{
    internal override GeneratorResult Generate(int item, DateTime entryDate)
    {
        var notification = GetNotificationBuilder("chedp-one-commodity")
            .WithEntryDate(entryDate)
            .WithReferenceNumber(ImportNotificationTypeEnum.Cvedp)
            .WithCommodity("1604142800", "Skipjack Tuna", 300)
            .ValidateAndBuild();
        
        logger.LogInformation("Created {@Notification}, {NotificationReferenceNumber}", notification, notification.ReferenceNumber);
        
        var clearanceRequest = GetClearanceRequestBuilder("cr-one-item")
            .WithEntryDate(entryDate)
            .WithReferenceNumber(notification.ReferenceNumber!)
            .WithItem("N853", "16041428", "Skipjack Tuna Other ROW CHEDP", 300)
            .ValidateAndBuild();
        
        logger.LogInformation("Created {@ClearanceRequest}, {EntryReference}", clearanceRequest, clearanceRequest.Header!.EntryReference);

        return new GeneratorResult
        {
            ClearanceRequests = [clearanceRequest], ImportNotifications = [notification]
        };
    }
}
