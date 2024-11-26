using Cdms.Analytics.Tests.Helpers;
using Cdms.Backend.Data;
using Cdms.Backend.Data.Extensions;
using Cdms.Business.Extensions;
using Cdms.Consumers.Extensions;
using Cdms.Model.Extensions;
using Cdms.SyncJob.Extensions;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestDataGenerator.Helpers;
using TestDataGenerator.Scenarios;
using Xunit;
using Xunit.Abstractions;

namespace Cdms.Analytics.Tests;

public class GetImportNotificationsByArrivalDateTests
{
    private readonly IHost app;
    
    private readonly ILogger<GetImportNotificationsByMatchStatusTests> logger;
    
    private readonly IMatchingAggregationService svc;
    
    private readonly IMongoDbContext mongoContext;
    
    public GetImportNotificationsByArrivalDateTests(ITestOutputHelper testOutputHelper)
    {
        var builder = TestContextHelper.CreateBuilder(testOutputHelper);
        
        app = builder.Build();
        logger = app.Services.GetRequiredService<ILogger<GetImportNotificationsByMatchStatusTests>>();
        svc = app.Services.GetRequiredService<IMatchingAggregationService>();
        mongoContext = app.Services.GetRequiredService<IMongoDbContext>();
    }
    
    [Fact]
    public async Task WhenCalledWithSmallMatchAndNoMatchSet_ReturnExpectedAggregation()
    {
        // await mongoContext.DropCollections();
        //
        // var scenario = app.CreateScenarioConfig<ChedASimpleMatchScenarioGenerator>(10, 2);
        // await app.PushToConsumers(scenario, 1);
        //
        // var noMatchScenario = app.CreateScenarioConfig<ChedANoMatchScenarioGenerator>(5, 2);
        // await app.PushToConsumers(noMatchScenario, 2);
        //
        // var chedPScenario = app.CreateScenarioConfig<ChedPSimpleMatchScenarioGenerator>(1, 1);
        // await app.PushToConsumers(chedPScenario, 3);
        
        var result = (await svc
            .GetImportNotificationMatchingByArrival())
            .ToList();
        
        logger.LogInformation(result.ToJsonString());
            
        result.Count.Should().Be(3);

        result[0].Name.Should().Be("Cveda Match");
        
        result[1].Name.Should().Be("Cveda No Match");
        
        result[2].Name.Should().Be("Cvedp Match");
    }
}