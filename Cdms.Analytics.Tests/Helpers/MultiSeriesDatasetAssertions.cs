using Cdms.Common.Extensions;
using FluentAssertions;
using FluentAssertions.Collections;

namespace Cdms.Analytics.Tests.Helpers;

public class MultiSeriesDatasetAssertions(List<MultiSeriesDataset>? test)
    : GenericCollectionAssertions<MultiSeriesDataset>(test)
{
    [CustomAssertion]
    public void BeSameLength(string because = "", params object[] becauseArgs)
    {
        test!.Select(r => r.Results.Count)
            .Distinct()
            .Count()
            .Should()
            .Be(1);
    }
    
    [CustomAssertion]
    public void HaveResults(string because = "", params object[] becauseArgs)
    {
        test!.Sum(d => d.Results.Sum(r => r.Value))
            .Should().BeGreaterThan(0);
    }
}