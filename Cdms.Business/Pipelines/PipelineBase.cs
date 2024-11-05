using MediatR;

namespace Cdms.Business.Pipelines;

public abstract class PipelineBase<TModel, TRequest> : IPipelineBehavior<TRequest, PipelineResult>
    where TRequest : PipelineRequest<TModel>
{
    public abstract Task<PipelineResult> ProcessMatch(TModel model);
    
    public async Task<PipelineResult> Handle(
        TRequest request,
        RequestHandlerDelegate<PipelineResult> next,
        CancellationToken cancellationToken)
    {
        var currentProgress = await ProcessMatch(request.Model);
        
        if (currentProgress.ExitPipeline)
        {
            return currentProgress;
        }
        
        return await next();
    }
}