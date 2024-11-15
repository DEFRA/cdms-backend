using Cdms.Types.Ipaffs;
using Microsoft.Extensions.Logging;

namespace TestDataGenerator.Scenarios;

internal class ChedAManyCommoditiesScenarioGenerator(ILogger<ChedAManyCommoditiesScenarioGenerator> logger) : ScenarioGenerator
{
    internal override GeneratorResult Generate(int item, DateTime entryDate)
    {
        // TODO : get a good 'pair' of notification and cr as the source templates
        
        var notification = GetNotificationBuilder("cheda-one-commodity")
            .WithEntryDate(entryDate)
            .WithReferenceNumber(ImportNotificationTypeEnum.Cveda, item)
            .WithRandomCommodities(10, 100)
            .ValidateAndBuild();
        
        logger.LogInformation("Created {@Notification}, {NotificationReferenceNumber}", notification, notification.ReferenceNumber);
        
        var clearanceRequest = GetClearanceRequestBuilder("cr-one-item")
            .WithEntryDate(entryDate)
            .WithReferenceNumber(notification.ReferenceNumber!)
            .ValidateAndBuild();
        
        logger.LogInformation("Created {@ClearanceRequest}, {EntryReference}", clearanceRequest, clearanceRequest.Header!.EntryReference);

        return new GeneratorResult()
        {
            ClearanceRequests = [clearanceRequest], ImportNotifications = [notification]
        };
    }
}