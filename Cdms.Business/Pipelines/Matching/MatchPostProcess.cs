using MediatR.Pipeline;

namespace Cdms.Business.Pipelines.Matching;

public class MatchPostProcess : IRequestPostProcessor<MatchRequest, MatchResult>
{
    public async Task Process(MatchRequest request, MatchResult response, CancellationToken cancellationToken)
    {
        request.Model.Record += "Did Post Processing" + Environment.NewLine;
    }
}