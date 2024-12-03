using Cdms.Common.Extensions;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Cdms.Analytics.Tests;

[Collection("Aggregation Test collection")]
public class GetImportNotificationsStatus(
    AggregationTestFixture aggregationTestFixture,
    ITestOutputHelper testOutputHelper)
{
    
    [Fact]
    public async Task WhenCalledLastWeek_ReturnExpectedAggregation()
    {
        testOutputHelper.WriteLine("Querying for aggregated data");
        var result = (await aggregationTestFixture.ImportNotificationsAggregationService
            .ByStatus(DateTime.Today.WeekAgo(), DateTime.Today.Tomorrow()));

        testOutputHelper.WriteLine("{0} aggregated items found", result.Values.Count);
        
        result.Values.Count.Should().Be(3);
        result.Values.Keys.Order().Should().Equal("CHEDA Linked", "CHEDA Not Linked", "CHEDP Linked");
    }
    
    [Fact]
    public async Task WhenCalledLast48Hours_ReturnExpectedAggregation()
    {
        testOutputHelper.WriteLine("Querying for aggregated data");
        var result = (await aggregationTestFixture.ImportNotificationsAggregationService
            .ByStatus(DateTime.Now.NextHour().AddDays(-2), DateTime.Now.NextHour()));

        testOutputHelper.WriteLine($"{result.Values.Count} aggregated items found");
        
        result.Values.Count.Should().Be(3);
        result.Values.Keys.Order().Should().Equal("CHEDA Linked", "CHEDA Not Linked", "CHEDP Linked");
    }
}