using MediatR;

namespace Cdms.Business.Pipelines.Matching;

public record MatchRequest(MatchModel Model) : PipelineRequest<MatchModel, MatchResult>(Model);