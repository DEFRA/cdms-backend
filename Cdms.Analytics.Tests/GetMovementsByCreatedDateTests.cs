using Cdms.Common.Extensions;
using Cdms.Model.Extensions;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Cdms.Analytics.Tests;

[Collection("Aggregation Test collection")]
public class GetMovementsByCreatedDateTests(
    AggregationTestFixture aggregationTestFixture,
    ITestOutputHelper testOutputHelper) {
   

    [Fact]
    public async Task WhenCalledLast24Hours_ReturnExpectedAggregation()
    {

// <<<<<<< HEAD
        var result = (await aggregationTestFixture.LinkingAggregationService
// =======
//         var scenario = app.CreateScenarioConfig<ChedASimpleMatchScenarioGenerator>(10, 2);
//         await app.PushToConsumers(scenario, 1);
//
//         var noMatchScenario = app.CreateScenarioConfig<ChedANoMatchScenarioGenerator>(5, 2);
//         await app.PushToConsumers(noMatchScenario, 2);
//
//         var chedPScenario = app.CreateScenarioConfig<ChedPSimpleMatchScenarioGenerator>(1, 1);
//         await app.PushToConsumers(chedPScenario, 3);
//
//         var result = (await svc
// >>>>>>> e4d59ec (More analytics)
            .GetMovementsLinkingByCreated(DateTime.Now.NextHour().AddDays(-1), DateTime.Now.NextHour(), AggregationPeriod.Hour))
            .ToList();

        testOutputHelper.WriteLine(result.ToJsonString());

        result.Count.Should().Be(1);

        result[0].Name.Should().Be("Linked");
        result[0].Periods[0].Period.Should().BeOnOrBefore(DateTime.Today);
        result[0].Periods.Count.Should().Be(24);
    }
}