using Cdms.Common.Extensions;
using Cdms.Model.Extensions;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using Cdms.Analytics.Tests.Fixtures;

namespace Cdms.Analytics.Tests;

[Collection(nameof(BasicSampleDataTestCollection))]
public class ImportNotificationsByCreatedDateTests(
    BasicSampleDataTestFixture basicSampleDataTestFixture,
    ITestOutputHelper testOutputHelper)
{
    
    [Fact]
    public async Task WhenCalledLast48Hours_ReturnExpectedAggregation()
    {
        var result = (await basicSampleDataTestFixture.ImportNotificationsAggregationService
            .ByCreated(DateTime.Now.NextHour().AddDays(-2), DateTime.Now.NextHour(),AggregationPeriod.Hour))
            .ToList();

        testOutputHelper.WriteLine(result.ToJsonString());

        result.Count.Should().Be(8);

        result[0].Periods.Count(p => p.Value > 0).Should().BeGreaterThan(1);

        result[0].Name.Should().Be("CHEDA Linked");
        result[0].Periods[0].Period.Should().BeOnOrBefore(DateTime.Today.Tomorrow());
        result[0].Periods.Count.Should().Be(48);
    }
    
    [Fact]
    public async Task WhenCalledLastMonth_ReturnExpectedAggregation()
    {
        var result = (await basicSampleDataTestFixture.ImportNotificationsAggregationService
            .ByCreated(DateTime.Today.MonthAgo(), DateTime.Today.Tomorrow()))
            .ToList();

        testOutputHelper.WriteLine(result.ToJsonString());

        result.Count.Should().Be(8);

        result[0].Name.Should().Be("CHEDA Linked");
        
        result.Should().AllSatisfy(r =>
        {
            r.Periods.Should().AllSatisfy(p =>
            {
                p.Period.Should().BeOnOrAfter(DateTime.Today.MonthAgo());
                p.Period.Should().BeOnOrBefore(DateTime.Today.Tomorrow());
            });
            r.Periods.Count.Should().Be(DateTime.Today.DaysSinceMonthAgo() + 1);
        });
    }
    
    [Fact]
    public async Task WhenCalledWithTimePeriodYieldingNoResults_ReturnEmptyAggregation()
    {
        DateTime from = DateTime.MaxValue.AddDays(-1);
        DateTime to = DateTime.MaxValue;

        var result = (await basicSampleDataTestFixture.ImportNotificationsAggregationService
                .ByCreated(from, to, AggregationPeriod.Hour))
            .ToList();

        testOutputHelper.WriteLine(result.ToJsonString());

        result.Count.Should().Be(8);

        result.Select(r => r.Name).Should().Equal(
            "CHEDA Linked", "CHEDA Not Linked", "CHEDD Linked", "CHEDD Not Linked", "CHEDP Linked", "CHEDP Not Linked", "CHEDPP Linked", "CHEDPP Not Linked");

        result.Should().AllSatisfy(r =>
        {
            r.Periods.Should().AllSatisfy(p =>
            {
                p.Period.Should().BeOnOrAfter(from);
                p.Period.Should().BeOnOrBefore(to);
            });
            r.Periods.Count.Should().Be(24);
        });
    }
}