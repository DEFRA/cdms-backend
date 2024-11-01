using Cdms.Business.Pipelines.Matching;
using Cdms.Business.Pipelines.Matching.Rules;
using FluentAssertions;
using Xunit;

namespace Cdms.Business.Tests.Pipelines.Matching.Rules;

public class Level1Rule2Tests
{
    [Fact]
    public async Task ProcessMatch_ValidModel_ReturnsNoMatch()
    {
        // Arrange
        var sut = new Level1Rule2();
        var model = new MatchModel();
        
        // Act
        var result = await sut.ProcessMatch(model);
        
        // Assert
        result.Should().NotBeNull();
        result.Complete.Should().BeFalse();
        model.Record.Should().StartWith("Did rule two");
    }
}