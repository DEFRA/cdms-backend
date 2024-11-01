namespace Cdms.Business.Pipelines.Matching.Rules;

public class Level1Rule8 : MatchPipelineBase
{
    public override async Task<MatchResult> ProcessMatch(MatchModel model)
    {
        model.Record += "Did rule eight" + Environment.NewLine;
        
        return await Task.FromResult(new MatchResult(false));
    }
}