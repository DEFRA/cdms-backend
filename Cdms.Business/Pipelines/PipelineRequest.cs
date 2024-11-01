using MediatR;

namespace Cdms.Business.Pipelines;

public record PipelineRequest<TModel, TResult>(TModel Model) : IRequest<TResult>;