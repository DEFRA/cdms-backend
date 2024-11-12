namespace Cdms.Business.Pipelines.Matching.Rules;

public class Level1Rule8 : MatchRuleBase
{
    public override async Task<PipelineResult> ProcessFilter(MatchContext context)
    {
        context.Record += "Did rule eight" + Environment.NewLine;
        
        return await Task.FromResult(new PipelineResult(false));
    }
}