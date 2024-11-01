using MediatR;

namespace Cdms.Business.Pipelines;

public abstract class PipelineBase<TModel, TRequest, TResult> : IPipelineBehavior<TRequest, TResult>
    where TRequest : PipelineRequest<TModel, TResult>
    where TResult : PipelineResult
{
    public abstract Task<TResult> ProcessMatch(TModel model);

    public async Task<TResult> Handle(
        TRequest request,
        RequestHandlerDelegate<TResult> next,
        CancellationToken cancellationToken)
    {
        var currentProgress = await ProcessMatch(request.Model);

        if (currentProgress.Complete)
        {
            return currentProgress;
        }

        return await next();
    }
}