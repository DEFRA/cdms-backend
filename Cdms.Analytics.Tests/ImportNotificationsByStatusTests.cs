using Cdms.Common.Extensions;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

using Cdms.Analytics.Tests.Fixtures;

namespace Cdms.Analytics.Tests;

[Collection(nameof(BasicSampleDataTestCollection))]
public class ImportNotificationsByStatusTests(
    BasicSampleDataTestFixture basicSampleDataTestFixture,
    ITestOutputHelper testOutputHelper)
{
    
    [Fact]
    public async Task WhenCalledLastWeek_ReturnExpectedAggregation()
    {
        testOutputHelper.WriteLine("Querying for aggregated data");
        var result = (await basicSampleDataTestFixture.ImportNotificationsAggregationService
            .ByStatus(DateTime.Today.WeekAgo(), DateTime.Today.Tomorrow()));

        testOutputHelper.WriteLine("{0} aggregated items found", result.Values.Count);
        
        result.Values.Count.Should().Be(8);
    }
    
    [Fact]
    public async Task WhenCalledLast48Hours_ReturnExpectedAggregation()
    {
        testOutputHelper.WriteLine("Querying for aggregated data");
        var result = (await basicSampleDataTestFixture.ImportNotificationsAggregationService
            .ByStatus(DateTime.Now.NextHour().AddDays(-2), DateTime.Now.NextHour()));

        testOutputHelper.WriteLine($"{result.Values.Count} aggregated items found");
        
        result.Values.Count.Should().Be(8);
        result.Values.Keys.Order().Should().Equal(
            "CHEDA Linked", "CHEDA Not Linked", "CHEDD Linked", "CHEDD Not Linked", "CHEDP Linked", "CHEDP Not Linked", "CHEDPP Linked", "CHEDPP Not Linked");

    }
    
    [Fact]
    public async Task WhenCalledWithTimePeriodYieldingNoResults_ReturnEmptyAggregation()
    {
        testOutputHelper.WriteLine("Querying for aggregated data");
        var result = (await basicSampleDataTestFixture.ImportNotificationsAggregationService
            .ByStatus(DateTime.MaxValue.AddDays(-1), DateTime.MaxValue));

        testOutputHelper.WriteLine($"{result.Values.Count} aggregated items found");
        
        result.Values.Count.Should().Be(8);
    }
}