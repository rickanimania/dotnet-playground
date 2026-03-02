namespace AnyVsCount.Core.Diagnostics;

public sealed class ScenarioRunResult
{
    public ScenarioRunResult(string scenarioName, int totalRecords, long elapsedTicks)
    {
        ScenarioName = scenarioName;
        TotalRecords = totalRecords;
        ElapsedTicks = elapsedTicks;
    }

    public string ScenarioName { get; }
    public int TotalRecords { get; }
    public long ElapsedTicks { get; }
}