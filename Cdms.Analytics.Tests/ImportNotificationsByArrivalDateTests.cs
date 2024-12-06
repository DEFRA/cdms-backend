using Cdms.Common.Extensions;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

using Cdms.Analytics.Tests.Fixtures;

namespace Cdms.Analytics.Tests;

[Collection(nameof(BasicSampleDataTestCollection))]
public class ImportNotificationsByArrivalDateTests(
    BasicSampleDataTestFixture basicSampleDataTestFixture,
    ITestOutputHelper testOutputHelper)
{
    
    [Fact]
    public async Task WhenCalledNextMonth_ReturnExpectedAggregation()
    {
        testOutputHelper.WriteLine("Querying for aggregated data");
        
        var result = (await basicSampleDataTestFixture.ImportNotificationsAggregationService
            .ByArrival(DateTime.Today, DateTime.Today.MonthLater()))
            .ToList();

        testOutputHelper.WriteLine($"{result.Count} aggregated items found");
            
        result.Count.Should().Be(8);
        
        result.Should().AllSatisfy(r =>
        {
            r.Periods.Should().AllSatisfy(p =>
            {
                p.Period.Should().BeOnOrAfter(DateTime.Today);
                p.Period.Should().BeOnOrBefore(DateTime.Today.MonthLater());
            });
            r.Periods.Count.Should().Be(DateTime.Today.DaysUntilMonthLater());
        });
    }
}