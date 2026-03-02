using System.Diagnostics;
using DeferredExecutionMaterialization.Core.Abstractions;
using DeferredExecutionMaterialization.Core.Diagnostics;

namespace DeferredExecutionMaterialization.Core.Services;

public sealed class ScenarioRunner
{
    private readonly IMaterializationDataSource _dataSource;

    public ScenarioRunner(IMaterializationDataSource dataSource)
    {
        _dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
    }

    public ScenarioRunResult RunBad()
    {
        var sw = Stopwatch.StartNew();
        _dataSource.ExecuteBad();
        sw.Stop();

        return new ScenarioRunResult
        {
            ScenarioName = "Bad(ToList)",
            TotalRecords = _dataSource.BadMaterializedRecords,
            ElapsedTicks = sw.ElapsedTicks
        };
    }

    public ScenarioRunResult RunGood()
    {
        var sw = Stopwatch.StartNew();
        _dataSource.ExecuteGood();
        sw.Stop();

        return new ScenarioRunResult
        {
            ScenarioName = "Good(Filter)",
            TotalRecords = _dataSource.GoodMaterializedRecords,
            ElapsedTicks = sw.ElapsedTicks
        };
    }
}