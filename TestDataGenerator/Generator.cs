using System.Security.Authentication;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using TestDataGenerator.Helpers;
using TestDataGenerator.Services;

namespace TestDataGenerator;

public class Generator(ILogger<Generator> logger, IBlobService blobService)
{
    internal async Task Cleardown(string rootPath)
    {
        await blobService.CleanAsync($"{rootPath}/");
    }

    internal async Task Generate(int scenario, int count, int days, ScenarioGenerator generator, string rootPath)
    {
        logger.LogInformation("Generating {Count}x{Days} {Generator}", count, days, generator.GetType().Name);

        for (var day = -days + 1; day <= 0; day++)
        {
            logger.LogInformation("Generating day {Day}", day);
            var entryDate = DateTime.Today.AddDays(day);

            for (var i = 0; i < count; i++)
            {
                logger.LogInformation("Generating item {Index}", i);

                var generatorResult = generator.Generate(scenario, i, entryDate);
                var uploadResult = await InsertToBlobStorage(generatorResult, rootPath);
                if (!uploadResult) throw new AuthenticationException("Error uploading item. Probably auth.");
            }
        }
    }

    private async Task<bool> InsertToBlobStorage(ScenarioGenerator.GeneratorResult result, string rootPath)
    {
        logger.LogInformation(
            "Uploading {ImportNotificationsLength} Notification(s) and {ClearanceRequestsLength} Clearance Request(s) to blob storage",
            result.ImportNotifications.Length, result.ClearanceRequests.Length);

        var importNotificationBlobItems = result.ImportNotifications.Select(n =>
            new BlobItem { Name = n.BlobPath(rootPath), Content = JsonSerializer.Serialize(n) });

        var alvsClearanceRequestBlobItems = result.ClearanceRequests.Select(cr =>
            new BlobItem { Name = cr.BlobPath(rootPath), Content = JsonSerializer.Serialize(cr) });

        var success = await blobService.CreateBlobsAsync(importNotificationBlobItems
            .Concat(alvsClearanceRequestBlobItems).ToArray<IBlobItem>());
        return success;
    }
}