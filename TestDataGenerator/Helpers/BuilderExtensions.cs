using Cdms.BlobService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TestDataGenerator.Scenarios;

namespace TestDataGenerator.Helpers;

public static class BuilderExtensions
{   
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddHttpClient();

        services.AddSingleton<ChedASimpleMatchScenarioGenerator>();
        services.AddSingleton<ChedAManyCommoditiesScenarioGenerator>();
        services.AddSingleton<ChedPSimpleMatchScenarioGenerator>();
                
        var blobOptionsValidatorDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IValidateOptions<BlobServiceOptions>))!;
        services.Remove(blobOptionsValidatorDescriptor);
        
        return services;
    }
}