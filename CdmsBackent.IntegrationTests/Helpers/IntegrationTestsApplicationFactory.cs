using Cdms.BlobService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Cdms.Backend.Data;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace CdmsBackend.IntegrationTests.Helpers;

public class IntegrationTestsApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var mongoDatabaseDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IMongoDatabase));
            services.Remove(mongoDatabaseDescriptor!);

            services.AddSingleton(sp =>
            {
                var options = sp.GetService<IOptions<MongoDbOptions>>();
                var settings = MongoClientSettings.FromConnectionString(options?.Value.DatabaseUri);
                var client = new MongoClient(settings);

                var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
                // convention must be registered before initialising collection
                ConventionRegistry.Register("CamelCase", camelCaseConvention, _ => true);

                var dbName = string.IsNullOrEmpty(DatabaseName) ? Random.Shared.Next().ToString() : DatabaseName;
                return client.GetDatabase($"Cdms_MongoDb_{dbName}_Test");
            });

            var blobServiceDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IBlobService));
            services.Remove(blobServiceDescriptor!);

            services.AddSingleton<IBlobService>(new LocalBlobService("../../../Fixtures"));

            services.AddLogging(lb => lb.AddXUnit(TestOutputHelper));
        });

        builder.UseEnvironment("Development");
    }

    internal ITestOutputHelper TestOutputHelper { get; set; } = null!;

    internal string DatabaseName { get; set; } = null!;

    public IMongoDbContext GetDbContext()
    {
        return Services.CreateScope().ServiceProvider.GetRequiredService<IMongoDbContext>();
    }

    public static async Task ClearDb(HttpClient client)
    {
        await client.GetAsync("mgmt/collections/drop");
    }
}