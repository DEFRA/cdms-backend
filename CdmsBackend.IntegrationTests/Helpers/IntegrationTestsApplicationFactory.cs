using Cdms.BlobService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System.IO;
using Cdms.Backend.Data;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace CdmsBackend.IntegrationTests.Helpers;

public class IntegrationTestsApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            configurationBuilder.Properties.Add("BlobServiceOptions:AzureClientId", "TestValue");
            configurationBuilder.Properties.Add("BlobServiceOptions:AzureTenantId", "TestValue");
            configurationBuilder.Properties.Add("BlobServiceOptions:AzureClientSecret", "TestValue");
        });
        builder.ConfigureServices(services =>
        {
            var mongoDatabaseDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IMongoDatabase))!;
            services.Remove(mongoDatabaseDescriptor);

            services.AddSingleton(sp =>
            {
                var options = sp.GetService<IOptions<MongoDbOptions>>()!;
                var settings = MongoClientSettings.FromConnectionString(options.Value.DatabaseUri);
                var client = new MongoClient(settings);

                var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
                // convention must be registered before initialising collection
                ConventionRegistry.Register("CamelCase", camelCaseConvention, _ => true);

                var dbName = string.IsNullOrEmpty(DatabaseName) ? Random.Shared.Next().ToString() : DatabaseName;
                return client.GetDatabase($"Cdms_MongoDb_{dbName}_Test");
            });

            var blobServiceDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IBlobService))!;
            services.Remove(blobServiceDescriptor);

            services.AddOptions<BlobServiceOptions>().Configure(o =>
            {
                o.CachePath = "../../../Fixtures";
            });

            var mockBlob = NSubstitute.Substitute.For<IBlobService>();
            services.AddKeyedSingleton<IBlobService>("base", mockBlob);
            services.AddSingleton<IBlobService, CachingBlobService>();

            services.AddLogging(lb => lb.AddXUnit(testOutputHelper));
        });

        builder.UseEnvironment("Development");
    }

    internal ITestOutputHelper testOutputHelper { get; set; }

    internal string DatabaseName { get; set; }

    public IMongoDbContext GetDbContext()
    {
        return Services.CreateScope().ServiceProvider.GetRequiredService<IMongoDbContext>();
    }

    public async Task ClearDb(HttpClient client)
    {
        await client.GetAsync("mgmt/collections/drop");
    }
}