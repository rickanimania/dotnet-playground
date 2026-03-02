using DotnetPlayground.Common.ConsoleUI;
using DotnetPlayground.Common.ConsoleUI.Reports;
using AnyVsCount.Console.Configuration;
using AnyVsCount.Core.Diagnostics;
using AnyVsCount.Core.Models;
using AnyVsCount.Core.Services;
using AnyVsCount.Infrastructure.DataSources;
using AnyVsCount.Infrastructure.Sqlite.Database;
using AnyVsCount.Infrastructure.Sqlite.DataSources;

const RunMode mode = RunMode.Stress;

var config = RunConfiguration.GetConfig(mode);

ConsoleShell.PrintHeader("Comparacao entre Count() > 0 e Any() para verificacao de existencia, evidenciando custo invisivel de varredura completa vs parada antecipada.");

// InMemory
RunAndPrint(
    title: $"==== INMEMORY (workFactor {config.WorkFactor}) - Any() vs Count() | Mode: {mode}",
    runner: new ScenarioRunner(
        dataSource: new AnyVsCountDataAccessSimulator(config.TotalRecords, config.MatchIndex, config.WorkFactor),
        useEvaluatedRecordsAsTotal: true
    ),
    datasetRecords: config.TotalRecords,
    showEvaluatedAsTotal: true
);
System.Console.WriteLine();

// SQLite
var dbPath = SqliteDatabaseFactory.GetDatabasePath("any-vs-count.db");
SqliteDatabaseFactory.EnsureDatabaseCreated(dbPath, totalRecords: config.TotalRecords, matchId: config.MatchIndex);

RunAndPrint(
    title: $"==== SQLITE (DAPPER) - Any() vs Count() | Mode: {mode}",
    runner: new ScenarioRunner(
        dataSource: new SqliteAnyVsCountDataSource(dbPath, config.TotalRecords),
        useEvaluatedRecordsAsTotal: true
    ),
    datasetRecords: config.TotalRecords,
    showEvaluatedAsTotal: false
);

System.Console.WriteLine();

ConsoleReportPrinter.WaitForExit();

static void RunAndPrint(string title, ScenarioRunner runner, int datasetRecords, bool showEvaluatedAsTotal)
{
    var bad = runner.RunBad();
    var good = runner.RunGood();

    ConsoleReportPrinter.PrintHeaderWithDataset(title);

    ConsoleReportPrinter.PrintRowWithDataset(Map(bad, datasetRecords, showEvaluatedAsTotal));
    ConsoleReportPrinter.PrintRowWithDataset(Map(good, datasetRecords, showEvaluatedAsTotal));

    // Summary permanece igual
    ConsoleReportPrinter.PrintSummary(
        Map(bad, datasetRecords, showEvaluatedAsTotal),
        Map(good, datasetRecords, showEvaluatedAsTotal)
    );
}

static ConsoleScenarioRunResult Map(ScenarioRunResult r, int datasetRecords, bool evaluatedIsTotal)
{
    var evaluated = evaluatedIsTotal ? r.TotalRecords : (int?)null;

    return new ConsoleScenarioRunResult
    {
        ScenarioName = r.ScenarioName,
        TotalRecords = r.TotalRecords,
        ElapsedTicks = r.ElapsedTicks,
        DatasetRecords = datasetRecords,
        EvaluatedRecords = evaluated
    };
}