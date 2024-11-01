using Cdms.Business.Pipelines;
using MediatR;

namespace Cdms.Business.Tests.Pipelines;

public class PipelineTestHelpers
{
    public delegate RequestHandlerDelegate<MockResult> NextDelegate();

    public interface IStubService
    {
        MockResult ProcessMatch(MockModel model);
    }
    
    public class MockPipeline : PipelineBase<MockModel, MockRequest, MockResult>
    {
        private readonly IStubService _stubService;
        
        public MockPipeline(IStubService stubService)
        {
            _stubService = stubService;
        }
        
        public override async Task<MockResult> ProcessMatch(MockModel model)
        {
            return await Task.FromResult(_stubService.ProcessMatch(model));
        }
    }

    public class MockTerminatePipeline : TerminatePipelineBase<MockRequest, MockResult>;

    public record MockRequest(MockModel Model) : PipelineRequest<MockModel, MockResult>(Model);
    
    public record MockResult() : PipelineResult(false);
    
    public record MockModel;
}