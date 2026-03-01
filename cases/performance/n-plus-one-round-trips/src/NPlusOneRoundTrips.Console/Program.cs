using DotnetPlayground.Common.ConsoleUI;
using DotnetPlayground.Common.ConsoleUI.Reports;
using NPlusOneRoundTrips.Console.Configuration;
using NPlusOneRoundTrips.Core.Diagnostics;
using NPlusOneRoundTrips.Core.Services;
using NPlusOneRoundTrips.Infrastructure.Sqlite.Database;
using NPlusOneRoundTrips.Infrastructure.Sqlite.DataSources;

const RunMode mode = RunMode.Demo;

var (totalRecords, inMemoryDelayMs) = RunConfiguration.GetConfig(mode);

ConsoleShell.PrintHeader("Comparacao de N+1 e round-trips utilizando simulacao em memoria e SQLite com Dapper.");

RunAndPrint(
    title: $"==== INMEMORY (delay {inMemoryDelayMs}ms) - N+1 e Round Trips",
    runner: new ScenarioRunner(new DataAccessSimulator(totalRecords, inMemoryDelayMs))
);

System.Console.WriteLine();

RunAndPrint(
    title: "==== SQLITE (DAPPER) - N+1 e Round Trips",
    runner: new ScenarioRunner(new SqliteRecordDataSource(
        SqliteDatabaseFactory.GetDatabasePath("nplusone-roundtrips.db"),
        totalRecords))
);

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