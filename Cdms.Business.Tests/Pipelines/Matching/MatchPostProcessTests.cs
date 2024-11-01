using Cdms.Business.Pipelines.Matching;
using Cdms.Business.Pipelines.Matching.Rules;
using FluentAssertions;
using System.Security.Cryptography;
using Xunit;

namespace Cdms.Business.Tests.Pipelines.Matching;

public class MatchPostProcessTests
{
    [Fact]
    public async Task Process_ValidRequest_RecordsPostProcessing()
    {
        // Arrange
        var sut = new MatchPostProcess();
        var request = new MatchRequest(new MatchModel());
        var result = new MatchResult(false);
        
        var expectedRecord = "Did Post Processing";
        
        // Act
        await sut.Process(request, result, CancellationToken.None);
        
        // Assert
        request.Model.Record.Should().StartWith(expectedRecord);
    }
}