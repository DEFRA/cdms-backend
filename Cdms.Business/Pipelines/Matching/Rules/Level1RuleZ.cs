using Cdms.Business.Pipelines;

namespace Cdms.Business.Pipelines.Matching.Rules;

public class Level1RuleZ : MatchPipelineBase
{
    public override async Task<MatchResult> ProcessMatch(MatchModel model)
    {
        model.Record += "Did rule Z" + Environment.NewLine;
        
        return await Task.FromResult(new MatchResult(false));
    }
}