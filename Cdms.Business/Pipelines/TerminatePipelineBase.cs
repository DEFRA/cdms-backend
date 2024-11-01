using Cdms.Business.Pipelines.Matching;
using MediatR;

namespace Cdms.Business.Pipelines;

public abstract class TerminatePipelineBase<TRequest, TResult> : IPipelineBehavior<TRequest, TResult>
    where TRequest : notnull
    where TResult : PipelineResult
{
    public async Task<TResult> Handle(
        TRequest request,
        RequestHandlerDelegate<TResult> next,
        CancellationToken cancellationToken)
    {
        var result = (TResult)Activator.CreateInstance(typeof(TResult), false)!;
        return await Task.FromResult(result);
    }
}