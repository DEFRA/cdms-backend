using MediatR;

namespace Cdms.Business.Pipelines;

public record PipelineRequest<TModel>(TModel Model) : IRequest<PipelineResult>;