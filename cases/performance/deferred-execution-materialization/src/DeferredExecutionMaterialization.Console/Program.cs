using DotnetPlayground.Common.ConsoleUI;
using DotnetPlayground.Common.ConsoleUI.Reports;
using DeferredExecutionMaterialization.Console.Configuration;
using DeferredExecutionMaterialization.Core.Models;
using DeferredExecutionMaterialization.Core.Services;
using DeferredExecutionMaterialization.Infrastructure.DataSources;
using DeferredExecutionMaterialization.Infrastructure.Sqlite.Database;
using DeferredExecutionMaterialization.Infrastructure.Sqlite.DataSources;

const RunMode mode = RunMode.Stress;

var (totalRecords, inMemoryDelayMs) = RunConfiguration.GetConfig(mode);

// InMemory
var inMemoryDataSource = new MaterializationDataAccessSimulator(totalRecords, inMemoryDelayMs);
var inMemoryRunner = new ScenarioRunner(inMemoryDataSource);

var inMemoryBad = inMemoryRunner.RunBad();
var inMemoryGood = inMemoryRunner.RunGood();

ConsoleShell.PrintHeader("Comparacao de materializacao precoce (ToList) e execucao tardia, avaliando custo invisivel no acesso a dados.");

ConsoleReportPrinter.PrintHeader($"==== INMEMORY (delay {inMemoryDelayMs}ms) - Materializacao e Execucao Tardia");

static ConsoleScenarioRunResult Map(dynamic r) => new()
{
    ScenarioName = r.ScenarioName,
    TotalRecords = r.TotalRecords,
    ElapsedTicks = r.ElapsedTicks
};

ConsoleReportPrinter.PrintRow(Map(inMemoryBad));
ConsoleReportPrinter.PrintRow(Map(inMemoryGood));
ConsoleReportPrinter.PrintSummary(Map(inMemoryBad), Map(inMemoryGood));

System.Console.WriteLine();

// SQLite
var dbPath = SqliteDatabaseFactory.GetDatabasePath("deferred-execution-materialization.db");
SqliteDatabaseFactory.EnsureDatabaseCreated(dbPath, totalRecords, activeRate: 0.25);

var sqliteDataSource = new SqliteMaterializationDataSource(dbPath, totalRecords);
var sqliteRunner = new ScenarioRunner(sqliteDataSource);

var sqliteBad = sqliteRunner.RunBad();
var sqliteGood = sqliteRunner.RunGood();

ConsoleReportPrinter.PrintHeader("==== SQLITE (DAPPER) - Materializacao e Execucao Tardia");

ConsoleReportPrinter.PrintRow(new ConsoleScenarioRunResult
{
    ScenarioName = sqliteBad.ScenarioName,
    TotalRecords = sqliteBad.TotalRecords,
    ElapsedTicks = sqliteBad.ElapsedTicks
});

ConsoleReportPrinter.PrintRow(new ConsoleScenarioRunResult
{
    ScenarioName = sqliteGood.ScenarioName,
    TotalRecords = sqliteGood.TotalRecords,
    ElapsedTicks = sqliteGood.ElapsedTicks
});

ConsoleReportPrinter.PrintSummary(
    new ConsoleScenarioRunResult
    {
        ScenarioName = sqliteBad.ScenarioName,
        TotalRecords = sqliteBad.TotalRecords,
        ElapsedTicks = sqliteBad.ElapsedTicks
    },
    new ConsoleScenarioRunResult
    {
        ScenarioName = sqliteGood.ScenarioName,
        TotalRecords = sqliteGood.TotalRecords,
        ElapsedTicks = sqliteGood.ElapsedTicks
    });

System.Console.WriteLine();

ConsoleReportPrinter.WaitForExit();