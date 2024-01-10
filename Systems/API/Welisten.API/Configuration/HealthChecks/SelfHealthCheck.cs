using System.Reflection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Welisten.API.Configuration.HealthChecks;

public class SelfHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, 
        CancellationToken cancellationToken = default)
    {
        var assembly = Assembly.Load("Welisten.API");
        var versionNumber = assembly.GetName().Version;

        return Task.FromResult(HealthCheckResult.Healthy($"Build {versionNumber}"));
    }
}