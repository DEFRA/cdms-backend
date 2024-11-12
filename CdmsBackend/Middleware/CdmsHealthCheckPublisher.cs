using Microsoft.Extensions.Diagnostics.HealthChecks;
using Cdms.Model.Extensions;
using HealthChecks.UI.Core;

namespace CdmsBackend.Middleware
{
    public class CdmsHealthCheckPublisher(ILogger<CdmsHealthCheckPublisher> logger) : IHealthCheckPublisher
    {
        public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
        {
            if (report.Status == HealthStatus.Unhealthy)
            {
                var uiReport = UIHealthReport.CreateFrom(report, e => e.ToString());
                logger.LogError("Service unhealthly {report}", uiReport.ToJsonString());
            }

            return Task.CompletedTask;
        }
    }
}
