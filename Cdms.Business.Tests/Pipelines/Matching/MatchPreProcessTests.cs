using Cdms.Business.Pipelines.Matching;
using Cdms.Business.Pipelines.Matching.Rules;
using FluentAssertions;
using Xunit;

namespace Cdms.Business.Tests.Pipelines.Matching;

public class MatchPreProcessTests
{
    [Fact]
    public async Task Process_ValidRequest_RecordsPreProcessing()
    {
        // Arrange
        var sut = new MatchPreProcess();
        var request = new MatchRequest(new MatchModel()
        {
            MatchReference = "ABC123"
        });
        
        var expectedRecord = $"Did pre-processing with initial request [{request.Model.MatchReference}]";
        
        // Act
        await sut.Process(request, CancellationToken.None);
        
        // Assert
        request.Model.Record.Should().StartWith(expectedRecord);
    }
}