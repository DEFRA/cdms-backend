using System.Runtime.InteropServices.JavaScript;
using Cdms.Business.Pipelines;
using Cdms.Business.Pipelines.Matching;
using FluentAssertions;
using MediatR;
using Moq;
using NSubstitute;
using Xunit;

namespace Cdms.Business.Tests.Pipelines;

public class PipelineBaseTests
{
    [Fact]
    public async Task Handle_SuccessfullyCompletedMatch_ExitsPipeline()
    {
        // Arrange
        var mockNextDelegate = new Mock<RequestHandlerDelegate<PipelineTestHelpers.MockResult>>();
        mockNextDelegate.Setup(x => x());

        var stubService = new Mock<PipelineTestHelpers.IStubService>();
        stubService.Setup(s => s.ProcessMatch(It.IsAny<PipelineTestHelpers.MockModel>()))
            .Returns(new PipelineTestHelpers.MockResult()
            {
                Complete = true
            });

        var sut = new PipelineTestHelpers.MockPipeline(stubService.Object);
        var request = new PipelineTestHelpers.MockRequest(new PipelineTestHelpers.MockModel());

        // Act
        var result = await sut.Handle(request, mockNextDelegate.Object, CancellationToken.None);

        // Assert
        result.Complete.Should().BeTrue();
        mockNextDelegate.Verify(x => x.Invoke(), Times.Never);
    }

    [Fact]
    public async Task Handle_UnsuccessfulMatch_ContinuesPipeline()
    {
        // Arrange
        var mockNextDelegate = new Mock<RequestHandlerDelegate<PipelineTestHelpers.MockResult>>();
        mockNextDelegate.Setup(x => x())
            .ReturnsAsync(new PipelineTestHelpers.MockResult()
            {
                Complete = true
            });

        var stubService = new Mock<PipelineTestHelpers.IStubService>();
        stubService.Setup(s => s.ProcessMatch(It.IsAny<PipelineTestHelpers.MockModel>()))
            .Returns(new PipelineTestHelpers.MockResult()
            {
                Complete = false
            });
        
        var sut = new PipelineTestHelpers.MockPipeline(stubService.Object);
        var request = new PipelineTestHelpers.MockRequest(new PipelineTestHelpers.MockModel());
        
        // Act
        var result = await sut.Handle(request, mockNextDelegate.Object, CancellationToken.None);
        
        // Assert
        result.Complete.Should().BeTrue();
        mockNextDelegate.Verify(x => x.Invoke(), Times.Once);
    }
}