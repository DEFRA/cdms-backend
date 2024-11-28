using Cdms.Common.Extensions;
using Cdms.Model.Extensions;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Cdms.Analytics.Tests;

[Collection("Aggregation Test collection")]
public class GetImportNotificationsByCreatedDateTests(
    AggregationTestFixture aggregationTestFixture,
    ITestOutputHelper testOutputHelper)
{
    
    [Fact]
    public async Task WhenCalledLast24Hours_ReturnExpectedAggregation()
    {
       
        
        var result = (await aggregationTestFixture.LinkingAggregationService
                .GetImportNotificationLinkingByCreated(DateTime.Now.NextHour().AddDays(-1), DateTime.Now.NextHour(), AggregationPeriod.Hour))
            .ToList();

        testOutputHelper.WriteLine(result.ToJsonString());

        result.Count.Should().Be(3);

        result[0].Periods.Count(p => p.Value > 0).Should().BeGreaterThan(1);

        result[0].Name.Should().Be("Cveda Linked");
        result[0].Periods[0].Period.Should().BeOnOrBefore(DateTime.Today);
        result[0].Periods.Count.Should().Be(24);
        
        result[1].Name.Should().Be("Cveda Not Linked");
        result[1].Periods[0].Period.Should().BeOnOrBefore(DateTime.Today);
        result[1].Periods.Count.Should().Be(24);
        
        result[2].Name.Should().Be("Cvedp Linked");
        result[2].Periods[0].Period.Should().BeOnOrBefore(DateTime.Today);
        result[2].Periods.Count.Should().Be(24);
    }
    
    [Fact]
    public async Task WhenCalledLast30Days_ReturnExpectedAggregation()
    {
        var result = (await aggregationTestFixture.LinkingAggregationService
            .GetImportNotificationLinkingByCreated(DateTime.Today.AddDays(-30), DateTime.Today))
            .ToList();

        testOutputHelper.WriteLine(result.ToJsonString());

        result.Count.Should().Be(3);

        result[0].Name.Should().Be("Cveda Linked");
        result[0].Periods[0].Period.Should().BeOnOrBefore(DateTime.Today);
        result[0].Periods.Count.Should().Be(30);
        
        result[1].Name.Should().Be("Cveda Not Linked");
        result[1].Periods[0].Period.Should().BeOnOrBefore(DateTime.Today);
        result[1].Periods.Count.Should().Be(30);
        
        result[2].Name.Should().Be("Cvedp Linked");
        result[2].Periods[0].Period.Should().BeOnOrBefore(DateTime.Today);
        result[2].Periods.Count.Should().Be(30);
    }
}