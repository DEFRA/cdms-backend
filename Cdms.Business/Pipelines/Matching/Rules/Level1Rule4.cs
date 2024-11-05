namespace Cdms.Business.Pipelines.Matching.Rules;

public class Level1Rule4 : MatchPipelineBase
{
    public override async Task<PipelineResult> ProcessMatch(MatchModel model)
    {
        model.Record += "Did rule four" + Environment.NewLine;
        
        return await Task.FromResult(new PipelineResult(false));
    }
}