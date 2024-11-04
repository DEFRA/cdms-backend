using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace Cdms.Business.Tests.Pipelines;

public class PipelineBaseTests
{
    [Fact]
    public async Task Handle_SuccessfullyCompletedMatch_ExitsPipeline()
    {
        // Arrange
        var mockNextDelegate = Substitute.For<RequestHandlerDelegate<PipelineTestHelpers.MockResult>>();

        var stubService = Substitute.For<PipelineTestHelpers.IStubService>();
        stubService.ProcessMatch(Arg.Any<PipelineTestHelpers.MockModel>()).Returns(new PipelineTestHelpers.MockResult() { Complete = true });

        var sut = new PipelineTestHelpers.MockPipeline(stubService);
        var request = new PipelineTestHelpers.MockRequest(new PipelineTestHelpers.MockModel());

        // Act
        var result = await sut.Handle(request, mockNextDelegate, CancellationToken.None);

        // Assert
        result.Complete.Should().BeTrue();
        await mockNextDelegate.DidNotReceive().Invoke();
    }

    [Fact]
    public async Task Handle_UnsuccessfulMatch_ContinuesPipeline()
    {
        // Arrange
        
        var mockNextDelegate = Substitute.For<RequestHandlerDelegate<PipelineTestHelpers.MockResult>>();
        mockNextDelegate.Invoke().Returns(new PipelineTestHelpers.MockResult() { Complete = true });
        
        var stubService = Substitute.For<PipelineTestHelpers.IStubService>();
        stubService.ProcessMatch(Arg.Any<PipelineTestHelpers.MockModel>()).Returns(new PipelineTestHelpers.MockResult() { Complete = false });

        var sut = new PipelineTestHelpers.MockPipeline(stubService);
        var request = new PipelineTestHelpers.MockRequest(new PipelineTestHelpers.MockModel());
        
        // Act
        var result = await sut.Handle(request, mockNextDelegate, CancellationToken.None);
        
        // Assert
        result.Complete.Should().BeTrue();
        await mockNextDelegate.Received(1).Invoke();
    }
}