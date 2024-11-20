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
        logger.LogInformation($"Generating {count}x{days} {generator}.");

        for (var d = -days + 1; d <= 0; d++)
        {
            logger.LogInformation($"Generating day {d}");
            var entryDate = DateTime.Today.AddDays(d);

            for (var i = 0; i < count; i++)
            {
                logger.LogInformation($"Generating item {i}");

                var generatorResult = generator.Generate(scenario, i, entryDate);
                var uploadResult = await InsertToBlobStorage(generatorResult, rootPath);
                if (!uploadResult) throw new AuthenticationException("Error uploading item. Probably auth.");
            }
        }
    }

    internal async Task<bool> InsertToBlobStorage(ScenarioGenerator.GeneratorResult result, string rootPath)
    {
        logger.LogInformation(
            $"Uploading {result.ImportNotifications.Length} Notification(s) and {result.ClearanceRequests.Length} Clearance Request(s) to blob storage");

        var importNotificationBlobItems = result.ImportNotifications.Select(n =>
            new BlobItem { Name = n.BlobPath(rootPath), Content = JsonSerializer.Serialize(n) });

        var alvsClearanceRequestBlobItems = result.ClearanceRequests.Select(cr =>
            new BlobItem { Name = cr.BlobPath(rootPath), Content = JsonSerializer.Serialize(cr) });

        var success =
            await blobService.CreateBlobsAsync(importNotificationBlobItems.Concat(alvsClearanceRequestBlobItems)
                .ToArray());
        return success;
    }
}