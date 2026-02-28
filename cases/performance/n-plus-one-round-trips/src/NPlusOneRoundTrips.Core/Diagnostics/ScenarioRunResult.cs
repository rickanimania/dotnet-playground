namespace NPlusOneRoundTrips.Core.Diagnostics;

public sealed class ScenarioRunResult
{
    public string ScenarioName { get; init; } = string.Empty;
    public int TotalRecords { get; init; }
    public long ElapsedTicks { get; init; }
}