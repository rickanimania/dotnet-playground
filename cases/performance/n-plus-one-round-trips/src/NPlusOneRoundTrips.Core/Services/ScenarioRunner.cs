using System.Diagnostics;
using NPlusOneRoundTrips.Core.Diagnostics;
using NPlusOneRoundTrips.Core.Services.Scenarios;

namespace NPlusOneRoundTrips.Core.Services;

public sealed class ScenarioRunner
{
    private readonly int _totalRecords;

    public ScenarioRunner(int totalRecords)
    {
        _totalRecords = totalRecords;
    }

    public ScenarioRunResult RunBad()
    {
        var dataAccess = new DataAccessSimulator(_totalRecords);
        var scenario = new BadScenario(dataAccess);

        var sw = Stopwatch.StartNew();
        var result = scenario.Execute();
        sw.Stop();

        return new ScenarioRunResult
        {
            ScenarioName = "Bad",
            TotalRecords = result.Count,
            ElapsedMilliseconds = sw.ElapsedMilliseconds
        };
    }

    public ScenarioRunResult RunGood()
    {
        var dataAccess = new DataAccessSimulator(_totalRecords);
        var scenario = new GoodScenario(dataAccess);

        var sw = Stopwatch.StartNew();
        var result = scenario.Execute();
        sw.Stop();

        return new ScenarioRunResult
        {
            ScenarioName = "Good",
            TotalRecords = result.Count,
            ElapsedMilliseconds = sw.ElapsedMilliseconds
        };
    }
}