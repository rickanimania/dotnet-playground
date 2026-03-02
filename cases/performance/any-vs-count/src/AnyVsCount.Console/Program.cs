using DotnetPlayground.Common.ConsoleUI;
using DotnetPlayground.Common.ConsoleUI.Reports;
using AnyVsCount.Console.Configuration;
using AnyVsCount.Core.Diagnostics;
using AnyVsCount.Core.Models;
using AnyVsCount.Core.Services;
using AnyVsCount.Infrastructure.DataSources;
using AnyVsCount.Infrastructure.Sqlite.Database;
using AnyVsCount.Infrastructure.Sqlite.DataSources;

SQLitePCL.Batteries.Init();

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
    evaluatedIsTotal: true
);

System.Console.WriteLine();

// SQLite
var dbPath = SqliteDatabaseFactory.GetDatabasePath("any-vs-count.db");
SqliteDatabaseFactory.EnsureDatabaseCreated(dbPath, totalRecords: config.TotalRecords, matchId: config.MatchIndex);

RunAndPrint(
    title: $"==== SQLITE (DAPPER) - Any() vs Count() | Mode: {mode}",
    runner: new ScenarioRunner(
        dataSource: new SqliteAnyVsCountDataSource(dbPath, config.TotalRecords),
        useEvaluatedRecordsAsTotal: false
    ),
    datasetRecords: config.TotalRecords,
    evaluatedIsTotal: false
);

System.Console.WriteLine();
ConsoleReportPrinter.WaitForExit();

static void RunAndPrint(string title, ScenarioRunner runner, int datasetRecords, bool evaluatedIsTotal)
{
    var bad = runner.RunBad();
    var good = runner.RunGood();

    ConsoleReportPrinter.PrintHeaderWithDataset(title);

    var badMapped = Map(bad, datasetRecords, evaluatedIsTotal);
    var goodMapped = Map(good, datasetRecords, evaluatedIsTotal);

    ConsoleReportPrinter.PrintRowWithDataset(badMapped);
    ConsoleReportPrinter.PrintRowWithDataset(goodMapped);
    ConsoleReportPrinter.PrintSummary(badMapped, goodMapped);
}

static ConsoleScenarioRunResult Map(ScenarioRunResult r, int datasetRecords, bool evaluatedIsTotal) => new()
{
    ScenarioName = r.ScenarioName,
    TotalRecords = r.TotalRecords,
    ElapsedTicks = r.ElapsedTicks,
    DatasetRecords = datasetRecords,
    EvaluatedRecords = evaluatedIsTotal ? r.TotalRecords : null
};