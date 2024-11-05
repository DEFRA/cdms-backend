using Cdms.Business.Pipelines;
using MediatR;

namespace Cdms.Business.Tests.Pipelines;

public class PipelineTestHelpers
{
    public interface IStubService
    {
        PipelineResult ProcessMatch(MockModel model);
    }
    
    public class MockPipeline : PipelineBase<MockModel, MockRequest>
    {
        private readonly IStubService _stubService;
        
        public MockPipeline(IStubService stubService)
        {
            _stubService = stubService;
        }
        
        public override async Task<PipelineResult> ProcessMatch(MockModel model)
        {
            return await Task.FromResult(_stubService.ProcessMatch(model));
        }
    }

    public class MockTerminatePipeline : TerminatePipelineBase<MockRequest>;

    public record MockRequest(MockModel Model) : PipelineRequest<MockModel>(Model);

    public record MockModel;
}