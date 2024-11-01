namespace Cdms.Business.Pipelines.Matching;

public record MatchResult(bool Complete) : PipelineResult(Complete);