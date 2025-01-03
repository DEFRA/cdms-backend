using Cdms.Common.Extensions;
using Cdms.Model.Extensions;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

using Cdms.Analytics.Tests.Fixtures;

namespace Cdms.Analytics.Tests;

[Collection(nameof(BasicSampleDataTestCollection))]
public class MovementsByArrivalDateTests(
    BasicSampleDataTestFixture basicSampleDataTestFixture,
    ITestOutputHelper testOutputHelper) {
    
    [Fact]
    public async Task WhenCalledNext72Hours_ReturnExpectedAggregation()
    {

        var result = (await basicSampleDataTestFixture.MovementsAggregationService
            .ByArrival(DateTime.Now.CurrentHour(), DateTime.Now.CurrentHour().AddDays(3), AggregationPeriod.Hour))
            .ToList();

        testOutputHelper.WriteLine(result.ToJsonString());

        result.Select(r => r.Name).Order().Should().Equal("Linked", "Not Linked");

        result[0].Periods[0].Period.Should().BeOnOrAfter(DateTime.Today);
        result[0].Periods.Count.Should().Be(72);
    }
    
    [Fact]
    public async Task WhenCalledNextMonth_ReturnExpectedAggregation()
    {
        var result = (await basicSampleDataTestFixture.MovementsAggregationService
            .ByArrival(DateTime.Today, DateTime.Today.MonthLater()))
            .ToList();

        testOutputHelper.WriteLine(result.ToJsonString());

        result.Count.Should().Be(2);

        result[0].Name.Should().Be("Linked");
        result[0].Periods[0].Period.Should().BeOnOrAfter(DateTime.Today);
        result[0].Periods.Count.Should().Be(DateTime.Today.DaysUntilMonthLater());
        
        result[1].Name.Should().Be("Not Linked");
    }
}