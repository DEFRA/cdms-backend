using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TestDataGenerator;

public static class Logging
{
    public static IHostBuilder AddLogging(this IHostBuilder builder)
    {
        // Serilog - WIP
        return builder
            .ConfigureLogging((c, l) =>
                {
                    l.AddConsole();
                }
            );
    }
}