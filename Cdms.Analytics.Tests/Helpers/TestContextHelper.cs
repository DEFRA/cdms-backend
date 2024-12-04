using Cdms.Backend.Data.Extensions;
using Cdms.Analytics.Extensions;
using Cdms.Business.Extensions;
using Cdms.Consumers.Extensions;
using Cdms.SyncJob.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestDataGenerator.Helpers;
using Xunit.Abstractions;

namespace Cdms.Analytics.Tests.Helpers;

public static class TestContextHelper
{
    public static IHostBuilder CreateBuilder<T>(ITestOutputHelper testOutputHelper = null!)
    {
        var builder = Host.CreateDefaultBuilder();

        // Any integration test overrides could be added here
        // And we don't want to load the backend ini file 
        var configurationValues = new Dictionary<string, string>
        {
            { "DisableLoadIniFile", "true" },
            { "Mongo:DatabaseUri", "mongodb://127.0.0.1:29017?retryWrites=false" },
            { "Mongo:DatabaseName", $"Cdms_{typeof(T).Name}" },
            
            // TO-DO these aren't relevant to us, but cause an error
            // if not specified
            { "BlobServiceOptions:CachePath", "../../../Fixtures" },
            { "BlobServiceOptions:CacheReadEnabled", "true" }
        };

        builder
            .ConfigureAppConfiguration(c => c.AddInMemoryCollection(configurationValues!))
            .ConfigureServices((hostContext, s) =>
            {
                s.AddAnalyticsServices(hostContext.Configuration);
                s.ConfigureTestGenerationServices();
                s.AddMongoDbContext(hostContext.Configuration);
                s.AddBusinessServices(hostContext.Configuration);
                s.AddConsumers(hostContext.Configuration);
                s.AddSyncJob();
                if (testOutputHelper is not null)
                {
                    s.AddLogging(lb => lb.AddXUnit(testOutputHelper));
                }
            });

        return builder;
    }
}