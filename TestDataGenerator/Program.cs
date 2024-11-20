using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestDataGenerator.Config;
using TestDataGenerator.Scenarios;
using TestDataGenerator.Services;

namespace TestDataGenerator;

internal class Program
{
    static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddIniFile("Properties/local.env", true)
            .Build();

        var generatorConfig = new GeneratorConfig(configuration);

        var builder = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(builder =>
            {
                builder.Sources.Clear();
                builder.AddConfiguration(configuration);
            })
            .ConfigureServices((_, services) =>
            {
                services.AddHttpClient();

                services.AddSingleton<GeneratorConfig>(_ => generatorConfig);
                services.AddSingleton<ChedASimpleMatchScenarioGenerator>();
                services.AddSingleton<ChedAManyCommoditiesScenarioGenerator>();
                services.AddSingleton<ChedPSimpleMatchScenarioGenerator>();
                if (generatorConfig.StorageService == StorageService.Local)
                {
                    services.AddSingleton<IBlobService, LocalBlobService>();
                }
                else
                {
                    services.AddSingleton<IBlobService, BlobService>();
                }

                services.AddTransient<Generator>();
            })
            .AddLogging();


        Console.WriteLine("Welcome to test data generator.");

        var app = builder.Build();
        // var config = app.Services.GetRequiredService<GeneratorConfig>();
        var generator = app.Services.GetRequiredService<Generator>();
        var logger = app.Services.GetRequiredService<ILogger<Program>>();

        var datasets = new[]
        {
            new
            {
                Dataset = "LoadTest",
                RootPath = "GENERATED-LOADTEST-BASIC",
                Scenarios = new[]
                {
                    app.CreateScenarioConfig<ChedASimpleMatchScenarioGenerator>(3, 7),
                    app.CreateScenarioConfig<ChedAManyCommoditiesScenarioGenerator>(3, 7)
                }
            },
            new
            {
                Dataset = "LoadTest-90Dx10k",
                RootPath = "GENERATED-LOADTEST-90Dx10k",
                Scenarios = new[]
                {
                    app.CreateScenarioConfig<ChedASimpleMatchScenarioGenerator>(10000, 90),
                    app.CreateScenarioConfig<ChedAManyCommoditiesScenarioGenerator>(100, 90)
                }
            },
            new
            {
                Dataset = "PHA",
                RootPath = "GENERATED-PHA",
                Scenarios = new[]
                {
                    app.CreateScenarioConfig<ChedASimpleMatchScenarioGenerator>(10, 30),
                    app.CreateScenarioConfig<ChedAManyCommoditiesScenarioGenerator>(10, 30)
                }
            },
            new
            {
                Dataset = "IBM-SCENARIO-1",
                RootPath = "IBM-SCENARIO-1-CHEDP-1-ITEM-1-CR",
                Scenarios = new[]
                {
                    app.CreateScenarioConfig<ChedPSimpleMatchScenarioGenerator>(10, 30)
                }
            }
        };

        logger.LogInformation("{DatasetsCount} dataset(s) configured", datasets.Length);

        // Allows us to filter the sets and scenarios we want to run at any given time
        // Could be fed by CLI for example
        var setsToRun = datasets
            .Where(d => d.Dataset == "IBM-SCENARIO-1");
            // .Select(d => new {
            //     Scenarios = d.Scenarios
            //         .Where(s =>
            //             s.Name == "ChedASimpleMatch"
            //         ).ToArray(),
            //     d.Dataset,
            //     d.RootPath
            // });

        foreach (var dataset in setsToRun)
        {
            logger.LogInformation("{ScenariosCount} scenario(s) configured", dataset.Scenarios.Count());

            logger.LogInformation("Clearing down storage path");
            await generator.Cleardown(dataset.RootPath);
            logger.LogInformation("Cleared down");

            foreach (var s in dataset.Scenarios)
            {
                await generator.Generate(s.Count, s.Days, s.Generator, dataset.RootPath);
            }

            logger.LogInformation("{Dataset} Done", dataset.Dataset);
        }

        logger.LogInformation("Done");
    }
}