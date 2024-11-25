using Cdms.Analytics.Tests.Helpers;
using Cdms.Backend.Data;
using Cdms.Backend.Data.InMemory;
using Cdms.Model.Extensions;
using Cdms.Types.Alvs;
using Cdms.Types.Ipaffs;
using Cdms.Consumers.Extensions;
using Cdms.Business.Extensions;
using Cdms.Backend.Data.Extensions;
using Cdms.Consumers;
using Cdms.SyncJob.Extensions;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using NSubstitute;
using SlimMessageBus;
using SlimMessageBus.Host;
using Xunit;

using TestDataGenerator;
using TestDataGenerator.Helpers;
using TestDataGenerator.Scenarios;
using Xunit.Abstractions;

namespace Cdms.Analytics.Tests;

public class AggregationTests
{
    private readonly IHost app;
    
    private readonly ILogger<AggregationTests> logger;
    
    private readonly ISyncAggregationService svc;
    
    private readonly IMongoDbContext mongoContext;
    
    public AggregationTests(ITestOutputHelper testOutputHelper)
    {
        var builder = Host.CreateDefaultBuilder();
       
        // Any integration test overrides could be added here
        // And we don't want to load the backend ini file 
        var configurationValues = new Dictionary<string, string>
        {
            { "DisableLoadIniFile", "true" },
            { "Mongo:DatabaseUri", "mongodb://127.0.0.1:29017?retryWrites=false" },
            { "Mongo:DatabaseName", "Cdms_MongoDb_Aggregation_Test" },
            
            // TODO these aren't relevant to us, but cause an error
            // if not specified
            { "BlobServiceOptions:CachePath", "../../../Fixtures" },
            { "BlobServiceOptions:CacheReadEnabled", "true" }
        };

        builder
            .ConfigureAppConfiguration(c => c.AddInMemoryCollection(configurationValues!))
            .ConfigureServices((hostContext, s) =>
            {
                s.AddSingleton<ISyncAggregationService, SyncAggregationService>();
                s.ConfigureTestGenerationServices();
                s.AddMongoDbContext(hostContext.Configuration);
                s.AddBusinessServices(hostContext.Configuration);
                s.AddConsumers(hostContext.Configuration);
                s.AddSyncJob();
                s.AddLogging(lb => lb.AddXUnit(testOutputHelper));
            });
        
        app = builder.Build();
        logger = app.Services.GetRequiredService<ILogger<AggregationTests>>();
        svc = app.Services.GetRequiredService<ISyncAggregationService>();
        mongoContext = app.Services.GetRequiredService<IMongoDbContext>();
    }
    
    [Fact]
    public async Task WhenAllCalled_ReturnsXXX()
    {
        await mongoContext.DropCollections();
        var scenario = app.CreateScenarioConfig<ChedPSimpleMatchScenarioGenerator>(10, 2);
        await app.PushToConsumers(scenario, 1);
        
        var noMatchScenario = app.CreateScenarioConfig<ChedANoMatchScenarioGenerator>(5, 2);
        await app.PushToConsumers(noMatchScenario, 2);

        var result = (await svc
                .GetImportNotificationLinks())
            .OrderBy(r => r.Date)
            .ThenBy(r => r.BucketVariables["Matched"])
            .ToList();
        
        logger.LogInformation(result.ToJsonString());
            
        result.Count().Should().Be(4);
        
        result[0].Date.Should().Be(DateOnly.FromDateTime(DateTime.Today));
        result[0].Value.Should().Be(1);
        result[0].BucketVariables["Matched"].Should().Be("False");
        
        result[1].Date.Should().Be(DateOnly.FromDateTime(DateTime.Today));
        result[1].Value.Should().Be(3);
        result[1].BucketVariables["Matched"].Should().Be("True");
    }
}