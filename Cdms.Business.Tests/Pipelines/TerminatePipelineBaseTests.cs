using Cdms.Business.Pipelines;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace Cdms.Business.Tests.Pipelines;

public class TerminatePipelineBaseTests
{
    [Fact]
    public async Task Handle_ValidRequest_ShouldReturnFalse()
    {
        // Arrange
        var mockNextDelegate = new Mock<RequestHandlerDelegate<PipelineTestHelpers.MockResult>>();
        mockNextDelegate.Setup(x => x());

        var sut = new PipelineTestHelpers.MockTerminatePipeline();
        var request = new PipelineTestHelpers.MockRequest(new PipelineTestHelpers.MockModel());
        
        // Act
        var result = await sut.Handle(request, mockNextDelegate.Object, CancellationToken.None);

        // Assert
        result.Complete.Should().BeFalse();
        mockNextDelegate.Verify(x => x.Invoke(), Times.Never);
    }
}