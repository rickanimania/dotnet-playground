using DotnetPlayground.Common.ConsoleUI.Reports;
using NPlusOneRoundTrips.Core.Services;
using NPlusOneRoundTrips.Infrastructure.Sqlite.Database;
using NPlusOneRoundTrips.Infrastructure.Sqlite.DataSources;
using NPlusOneRoundTrips.Console.Configuration;
using DotnetPlayground.Common.ConsoleUI;


const RunMode mode = RunMode.Demo;

var (totalRecords, inMemoryDelayMs) = RunConfiguration.GetConfig(mode);

// InMemory
var inMemoryDataSource = new DataAccessSimulator(totalRecords, inMemoryDelayMs);
var inMemoryRunner = new ScenarioRunner(inMemoryDataSource);

var inMemoryBad = inMemoryRunner.RunBad();
var inMemoryGood = inMemoryRunner.RunGood();

ConsoleShell.PrintHeader("Comparacao de N+1 e round-trips utilizando simulacao em memoria e SQLite com Dapper.");

ConsoleReportPrinter.PrintHeader($"==== INMEMORY (delay {inMemoryDelayMs}ms) - N+1 e Round Trips");

var inMemoryBadRow = new ConsoleScenarioRunResult
{
    ScenarioName = inMemoryBad.ScenarioName,
    TotalRecords = inMemoryBad.TotalRecords,
    ElapsedTicks = inMemoryBad.ElapsedTicks
};

var inMemoryGoodRow = new ConsoleScenarioRunResult
{
    ScenarioName = inMemoryGood.ScenarioName,
    TotalRecords = inMemoryGood.TotalRecords,
    ElapsedTicks = inMemoryGood.ElapsedTicks
};

ConsoleReportPrinter.PrintRow(inMemoryBadRow);
ConsoleReportPrinter.PrintRow(inMemoryGoodRow);
ConsoleReportPrinter.PrintSummary(inMemoryBadRow, inMemoryGoodRow);

System.Console.WriteLine();

// SQLite
var dbPath = SqliteDatabaseFactory.GetDatabasePath("nplusone-roundtrips.db");
var sqliteDataSource = new SqliteRecordDataSource(dbPath, totalRecords);
var sqliteRunner = new ScenarioRunner(sqliteDataSource);

var sqliteBad = sqliteRunner.RunBad();
var sqliteGood = sqliteRunner.RunGood();

ConsoleReportPrinter.PrintHeader("==== SQLITE (DAPPER) - N+1 e Round Trips");

var sqliteBadRow = new ConsoleScenarioRunResult
{
    ScenarioName = sqliteBad.ScenarioName,
    TotalRecords = sqliteBad.TotalRecords,
    ElapsedTicks = sqliteBad.ElapsedTicks
};

var sqliteGoodRow = new ConsoleScenarioRunResult
{
    ScenarioName = sqliteGood.ScenarioName,
    TotalRecords = sqliteGood.TotalRecords,
    ElapsedTicks = sqliteGood.ElapsedTicks
};

ConsoleReportPrinter.PrintRow(sqliteBadRow);
ConsoleReportPrinter.PrintRow(sqliteGoodRow);
ConsoleReportPrinter.PrintSummary(sqliteBadRow, sqliteGoodRow);

ConsoleReportPrinter.WaitForExit();

