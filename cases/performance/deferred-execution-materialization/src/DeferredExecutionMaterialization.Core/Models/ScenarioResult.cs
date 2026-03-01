namespace DeferredExecutionMaterialization.Core.Models;

public sealed class ScenarioResult
{
    public required string ScenarioName { get; init; }
    public required RunMode Mode { get; init; }

    public int Items { get; init; }
    public int DelayMs { get; init; }

    public long ElapsedMilliseconds { get; init; }
}