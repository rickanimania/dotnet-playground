namespace DeferredExecutionMaterialization.Core.Diagnostics;

public sealed class ScenarioRunResult
{
    public required string ScenarioName { get; init; }
    public int TotalRecords { get; init; }
    public long ElapsedTicks { get; init; }
}