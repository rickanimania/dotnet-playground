using System.Diagnostics;
using AnyVsCount.Core.Abstractions;
using AnyVsCount.Core.Diagnostics;

namespace AnyVsCount.Core.Services;

public sealed class ScenarioRunner
{
    private readonly IExistenceDataSource _dataSource;
    private readonly bool _useEvaluatedRecordsAsTotal;

    public ScenarioRunner(IExistenceDataSource dataSource, bool useEvaluatedRecordsAsTotal)
    {
        _dataSource = dataSource;
        _useEvaluatedRecordsAsTotal = useEvaluatedRecordsAsTotal;
    }

    public ScenarioRunResult RunBad()
    {
        var sw = Stopwatch.StartNew();
        _dataSource.ExecuteBad();
        sw.Stop();

        var total = _useEvaluatedRecordsAsTotal
            ? _dataSource.BadEvaluatedRecords
            : _dataSource.TotalRecords;

        return new ScenarioRunResult("Bad(Count)", total, sw.ElapsedTicks);
    }

    public ScenarioRunResult RunGood()
    {
        var sw = Stopwatch.StartNew();
        _dataSource.ExecuteGood();
        sw.Stop();

        var total = _useEvaluatedRecordsAsTotal
            ? _dataSource.GoodEvaluatedRecords
            : _dataSource.TotalRecords;

        return new ScenarioRunResult("Good(Any)", total, sw.ElapsedTicks);
    }
}