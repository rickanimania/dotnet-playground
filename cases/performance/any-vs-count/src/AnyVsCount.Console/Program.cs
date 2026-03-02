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
    )
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
    )
);

System.Console.WriteLine();

ConsoleReportPrinter.WaitForExit();

static void RunAndPrint(string title, ScenarioRunner runner)
{
    var bad = runner.RunBad();
    var good = runner.RunGood();

    ConsoleReportPrinter.PrintHeader(title);
    ConsoleReportPrinter.PrintRow(Map(bad));
    ConsoleReportPrinter.PrintRow(Map(good));
    ConsoleReportPrinter.PrintSummary(Map(bad), Map(good));
}

static ConsoleScenarioRunResult Map(ScenarioRunResult r) => new()
{
    ScenarioName = r.ScenarioName,
    TotalRecords = r.TotalRecords,
    ElapsedTicks = r.ElapsedTicks
};