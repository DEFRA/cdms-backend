using Cdms.Analytics.Tests.Helpers;
using Cdms.Backend.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestDataGenerator.Scenarios;

namespace Cdms.Analytics.Tests;

#pragma warning disable S3881
public class AggregationTestFixture : IDisposable
#pragma warning restore S3881
{
    public IHost App;
    public ILinkingAggregationService LinkingAggregationService;

    public IMongoDbContext MongoDbContext;
    public AggregationTestFixture()
    {
        var builder = TestContextHelper.CreateBuilder<AggregationTestFixture>();

        App = builder.Build();
        var rootScope = App.Services.CreateScope();

        MongoDbContext = rootScope.ServiceProvider.GetRequiredService<IMongoDbContext>();
        LinkingAggregationService = rootScope.ServiceProvider.GetRequiredService<ILinkingAggregationService>();
        MongoDbContext.DropCollections().GetAwaiter().GetResult();

        var scenario = App.CreateScenarioConfig<ChedASimpleMatchScenarioGenerator>(10, 3, arrivalDateRange: 10);
        App.PushToConsumers(scenario, 1).GetAwaiter().GetResult();

        var noMatchScenario = App.CreateScenarioConfig<ChedANoMatchScenarioGenerator>(5, 3, arrivalDateRange: 10);
        App.PushToConsumers(noMatchScenario, 2).GetAwaiter().GetResult();

        var chedPScenario = App.CreateScenarioConfig<ChedPSimpleMatchScenarioGenerator>(1, 3, arrivalDateRange: 10);
        App.PushToConsumers(chedPScenario, 3).GetAwaiter().GetResult();



        SetupData().GetAwaiter().GetResult();
    }

    public async Task SetupData()
    {
        await MongoDbContext.DropCollections();

        var scenario = App.CreateScenarioConfig<ChedASimpleMatchScenarioGenerator>(10, 3, arrivalDateRange: 10);
        await App.PushToConsumers(scenario, 1);

        var noMatchScenario = App.CreateScenarioConfig<ChedANoMatchScenarioGenerator>(5, 3, arrivalDateRange: 10);
        await App.PushToConsumers(noMatchScenario, 2);

        var chedPScenario = App.CreateScenarioConfig<ChedPSimpleMatchScenarioGenerator>(1, 3, arrivalDateRange: 10);
        await App.PushToConsumers(chedPScenario, 3);
    }

    public void Dispose()
    {
        // ... clean up test data from the database ...
    }
}