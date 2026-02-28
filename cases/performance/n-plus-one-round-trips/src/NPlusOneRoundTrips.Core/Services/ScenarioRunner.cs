using System.Diagnostics;
using NPlusOneRoundTrips.Core.Abstractions;
using NPlusOneRoundTrips.Core.Diagnostics;
using NPlusOneRoundTrips.Core.Services.Scenarios;

namespace NPlusOneRoundTrips.Core.Services;

public sealed class ScenarioRunner
{
    private readonly IRecordDataSource _dataSource;

    public ScenarioRunner(IRecordDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public ScenarioRunResult RunBad()
    {
        var scenario = new BadScenario(_dataSource);

        var sw = Stopwatch.StartNew();
        var result = scenario.Execute();
        sw.Stop();

        return new ScenarioRunResult
        {
            ScenarioName = "Bad",
            TotalRecords = result.Count,
            ElapsedTicks = sw.ElapsedTicks
        };
    }

    public ScenarioRunResult RunGood()
    {
        var scenario = new GoodScenario(_dataSource);

        var sw = Stopwatch.StartNew();
        var result = scenario.Execute();
        sw.Stop();

        return new ScenarioRunResult
        {
            ScenarioName = "Good",
            TotalRecords = result.Count,
            ElapsedTicks = sw.ElapsedTicks
        };
    }
}