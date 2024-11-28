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
    public async Task WhenCalledLast7Days_ReturnExpectedAggregation()
    {


        testOutputHelper.WriteLine("Querying for aggregated data");
        var result = (await aggregationTestFixture.LinkingAggregationService
            .GetImportNotificationLinkingStatus(DateTime.Today.AddDays(-7), DateTime.Today));

        testOutputHelper.WriteLine("{0} aggregated items found", result.Values.Count);
        
        result.Values.Count.Should().Be(3);
        result.Values.Keys.Order().Should().Equal("Cveda Linked", "Cveda Not Linked", "Cvedp Linked");
    }
    
    [Fact]
    public async Task WhenCalledLast24Hours_ReturnExpectedAggregation()
    {

        testOutputHelper.WriteLine("Querying for aggregated data");
        var result = (await aggregationTestFixture.LinkingAggregationService
            .GetImportNotificationLinkingStatus(DateTime.Now.AddDays(-1), DateTime.Now));

        testOutputHelper.WriteLine($"{result.Values.Count} aggregated items found");
        
        result.Values.Count.Should().Be(3);
        result.Values.Keys.Order().Should().Equal("Cveda Linked", "Cveda Not Linked", "Cvedp Linked");
    }
}