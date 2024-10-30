using System.Text.Json;
using Cdms.Types.Alvs;
using Cdms.Types.Ipaffs;
using Microsoft.Extensions.Logging;

namespace TestDataGenerator.Scenarios;

internal class ChedAManyCommoditiesScenarioGenerator(ILogger<ChedAManyCommoditiesScenarioGenerator> logger) : ScenarioGenerator()
{
    internal override GeneratorResult Generate(int item)
    {
        // TODO : get a good 'pair' of notification and cr as the source templates
        
        var notification = GetNotificationBuilder("cheda-one-commodity")
            .WithReferenceNumber(ImportNotificationTypeEnum.Cveda, item)
            .WithRandomCommodities(10, 100)
            .Build()!;
        
        logger.LogInformation($"Created {notification}, {notification.ReferenceNumber}");
        
        var clearanceRequest = GetClearanceRequestBuilder("cr-one-item")
            .WithFirstReferenceNumber(notification.ReferenceNumber!)
            .Build();
        
        logger.LogInformation($"Created {clearanceRequest}, {clearanceRequest.Header!.EntryReference}");

        return new GeneratorResult()
        {
            ClearanceRequests = new[] { clearanceRequest }, ImportNotifications = new[] { notification }
        };
    }
}