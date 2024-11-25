using Cdms.Business.Commands;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CdmsBackend.Endpoints;

public static class AnalyticsEndpoints
{
    private const string BaseRoute = "analytics";

    public static void UseAnalyticsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(BaseRoute + "/sync", GetSyncAnalyticsAsync).AllowAnonymous();
    }
    private static async Task<IResult> GetSyncAnalyticsAsync(IMongoDatabase db)
    {
        var collections =
            (await (await db.ListCollectionsAsync()).ToListAsync()).ConvertAll(c 
                => new
                {
                    name = c["name"].ToString()!,
                    size = db.GetCollection<object>(c["name"].ToString()!).CountDocuments(Builders<object>.Filter.Empty)
                });
        
        return Results.Ok(new { collections = collections });
    }

}