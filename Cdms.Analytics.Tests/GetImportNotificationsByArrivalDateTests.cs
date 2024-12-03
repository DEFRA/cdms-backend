using Cdms.Common.Extensions;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Cdms.Analytics.Tests;

[Collection("Aggregation Test collection")]
public class GetImportNotificationsByArrivalDateTests(
    AggregationTestFixture aggregationTestFixture,
    ITestOutputHelper testOutputHelper)
{
    
    [Fact]
    public async Task WhenCalledNextMonth_ReturnExpectedAggregation()
    {
        testOutputHelper.WriteLine("Querying for aggregated data");
        
        var result = (await aggregationTestFixture.ImportNotificationsAggregationService
            .ByArrival(DateTime.Today, DateTime.Today.MonthLater()))
            .ToList();

        testOutputHelper.WriteLine($"{result.Count} aggregated items found");
        
        //// logger.LogInformation(result.ToJsonString());
            
        result.Count.Should().Be(3);

        result[0].Name.Should().Be("CHEDA Linked");
        result[0].Periods[0].Period.Should().BeOnOrAfter(DateTime.Today);
        result[0].Periods.Count.Should().Be(DateTime.Today.DaysUntilMonthLater());
        
        result[1].Name.Should().Be("CHEDA Not Linked");
        result[1].Periods[0].Period.Should().BeOnOrAfter(DateTime.Today);
        result[1].Periods.Count.Should().Be(DateTime.Today.DaysUntilMonthLater());

        result[2].Name.Should().Be("CHEDP Linked");
        result[2].Periods[0].Period.Should().BeOnOrAfter(DateTime.Today);
        result[2].Periods.Count.Should().Be(DateTime.Today.DaysUntilMonthLater());
    }
}