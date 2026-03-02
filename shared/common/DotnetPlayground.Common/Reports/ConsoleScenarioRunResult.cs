namespace DotnetPlayground.Common.ConsoleUI.Reports;

public sealed class ConsoleScenarioRunResult
{
    public required string ScenarioName { get; init; }

    public int TotalRecords { get; init; }

    public long ElapsedTicks { get; init; }
}