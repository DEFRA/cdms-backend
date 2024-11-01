using MediatR.Pipeline;

namespace Cdms.Business.Pipelines.Matching;

public class MatchPreProcess : IRequestPreProcessor<MatchRequest>
{
    public async Task Process(MatchRequest request, CancellationToken cancellationToken)
    {
        request.Model.Record += $"Did pre-processing with initial request [{request.Model.MatchReference}]{Environment.NewLine}";
    }
}