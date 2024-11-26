using Cdms.Analytics;
using Microsoft.AspNetCore.Mvc;

namespace CdmsBackend.Endpoints;

public static class AnalyticsEndpoints
{
	private const string BaseRoute = "analytics";
    
    public static void UseAnalyticsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(BaseRoute + "/import-notifications/matching-by-created", GetImportNotificationMatchingByCreated).AllowAnonymous();
        app.MapGet(BaseRoute + "/import-notifications/matching-by-arrival", GetImportNotificationMatchingByArrival).AllowAnonymous();
    }
    private static async Task<IResult> GetImportNotificationMatchingByCreated(
        [FromServices] IMatchingAggregationService svc)
    {
        var results = await svc.GetImportNotificationMatchingByCreated();
        
        return Results.Ok(new { results = results });
    }
    
    private static async Task<IResult> GetImportNotificationMatchingByArrival(
        [FromServices] IMatchingAggregationService svc)
    {
        var results = await svc.GetImportNotificationMatchingByArrival();
        
        return Results.Ok(new { results = results });
    }
}