using Cdms.Backend.Data;
using Cdms.Backend.Data.InMemory;
using Cdms.Model.Extensions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using MongoDB.Bson;
using NSubstitute;
using Xunit;

using TestDataGenerator;
using TestDataGenerator.Helpers;
using TestDataGenerator.Scenarios;

namespace Cdms.Analytics;

public class AggregationTests
{
    private readonly IHost app;
    
    private readonly ILogger<AggregationTests> logger;
    
    public AggregationTests()
    {
        var builder = Host.CreateDefaultBuilder();
        builder.ConfigureServices(s =>
        {
            s.AddSingleton<IMongoDbContext, MemoryMongoDbContext>();
            s.AddSingleton<ISyncAggregationService, SyncAggregationService>();
            s.ConfigureServices();
        });
        
        app = builder.Build();
        logger = app.Services.GetRequiredService<ILogger<AggregationTests>>();
    }
    
    [Fact]
    public async Task WhenAllCalled_ReturnsXXX()
    {
        var scenario = app.CreateScenarioConfig<ChedPSimpleMatchScenarioGenerator>(1, 1);
        var generatorResult = scenario.Generator.Generate(1, 1, DateTime.Today);
        
        logger.LogInformation(generatorResult.ToJsonString());
        
        ISyncAggregationService svc = app.Services.GetRequiredService<ISyncAggregationService>();
        var result = await svc.GetImportNotificationLinks();
        
        result.Length.Should().Be(1);
        result[0].Date.Should().Be(DateOnly.FromDateTime(DateTime.Today));
    }
}