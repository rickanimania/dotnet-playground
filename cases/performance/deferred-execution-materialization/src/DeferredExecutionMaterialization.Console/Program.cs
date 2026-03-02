using DotnetPlayground.Common.ConsoleUI;
using DotnetPlayground.Common.ConsoleUI.Reports;
using DeferredExecutionMaterialization.Console.Configuration;
using DeferredExecutionMaterialization.Core.Diagnostics;
using DeferredExecutionMaterialization.Core.Models;
using DeferredExecutionMaterialization.Core.Services;
using DeferredExecutionMaterialization.Infrastructure.DataSources;
using DeferredExecutionMaterialization.Infrastructure.Sqlite.Database;
using DeferredExecutionMaterialization.Infrastructure.Sqlite.DataSources;

const RunMode mode = RunMode.Stress;

var (totalRecords, inMemoryDelayMs) = RunConfiguration.GetConfig(mode);

ConsoleShell.PrintHeader("Comparacao de materializacao precoce (ToList) e execucao tardia, avaliando custo invisivel no acesso a dados.");

RunAndPrint(
    title: $"==== INMEMORY (delay {inMemoryDelayMs}ms) - Materializacao e Execucao Tardia",
    runner: new ScenarioRunner(new MaterializationDataAccessSimulator(totalRecords, inMemoryDelayMs))
);

System.Console.WriteLine();

// SQLite
var dbPath = SqliteDatabaseFactory.GetDatabasePath("deferred-execution-materialization.db");
SqliteDatabaseFactory.EnsureDatabaseCreated(dbPath, totalRecords, activeRate: 0.25);

RunAndPrint(
    title: "==== SQLITE (DAPPER) - Materializacao e Execucao Tardia",
    runner: new ScenarioRunner(new SqliteMaterializationDataSource(dbPath, totalRecords))
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