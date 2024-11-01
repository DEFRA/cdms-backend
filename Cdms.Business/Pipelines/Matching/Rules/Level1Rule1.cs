namespace Cdms.Business.Pipelines.Matching.Rules;

public class Level1Rule1 : MatchPipelineBase
{
    public override async Task<MatchResult> ProcessMatch(MatchModel model)
    {
        model.Record += "Did rule one" + Environment.NewLine;
        
        return await Task.FromResult(new MatchResult(false));
    }
}