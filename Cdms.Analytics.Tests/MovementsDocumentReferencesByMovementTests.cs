using Cdms.Common.Extensions;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

using Cdms.Analytics.Tests.Fixtures;
using Cdms.Analytics.Tests.Helpers;
    
namespace Cdms.Analytics.Tests;

[Collection(nameof(MultiItemDataTestCollection))]
public class MovementsDocumentReferencesByMovementTests(
    MultiItemDataTestFixture multiItemDataTestFixture,
    ITestOutputHelper testOutputHelper)
{
    [Fact]
    public async Task WhenCalledLastWeek_ReturnExpectedAggregation()
    {
        testOutputHelper.WriteLine("Querying for aggregated data");
        var result = (await multiItemDataTestFixture.MovementsAggregationService
            .UniqueDocumentReferenceByMovementCount(DateTime.Today.WeekAgo(), DateTime.Today.Tomorrow()));

        testOutputHelper.WriteLine("{0} aggregated items found", result.Values.Count);
        
        result.Should().HaveResults();
    }
}