namespace Cdms.Business.Pipelines.Matching.Rules;

public class Level1Rule2 : MatchPipelineBase
{
    public override async Task<PipelineResult> ProcessMatch(MatchModel model)
    {
        model.Record += "Did rule two" + Environment.NewLine;
        
        return await Task.FromResult(new PipelineResult(false));
    }
}