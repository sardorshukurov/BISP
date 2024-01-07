namespace Welisten.Common.HealthChecks;

public class HealthCheck
{
    public string OverallStatus { get; set; } = string.Empty;
    public IEnumerable<HealthCheckItem> HealthChecks { get; set; }
    public string TotalDuration { get; set; } = string.Empty;
}

public class HealthCheckItem
{
    public string Status { get; set; } = string.Empty;
    public string Component { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
}