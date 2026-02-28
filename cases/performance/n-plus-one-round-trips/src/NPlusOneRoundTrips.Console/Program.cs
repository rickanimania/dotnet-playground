using NPlusOneRoundTrips.Console.Presentation;
using NPlusOneRoundTrips.Core.Services;
using NPlusOneRoundTrips.Infrastructure.Sqlite.Database;
using NPlusOneRoundTrips.Infrastructure.Sqlite.DataSources;

const int totalRecords = 500;

// Para volumes altos, nao simule delay alto
var inMemoryRoundTripDelayMs = totalRecords <= 500 ? 2 : 0;

// InMemory
var inMemoryDataSource = new DataAccessSimulator(totalRecords, inMemoryRoundTripDelayMs);
var inMemoryRunner = new ScenarioRunner(inMemoryDataSource);

var inMemoryBad = inMemoryRunner.RunBad();
var inMemoryGood = inMemoryRunner.RunGood();

ConsoleReportPrinter.PrintHeader($"INMEMORY (delay {inMemoryRoundTripDelayMs}ms) - N+1 e Round Trips");
ConsoleReportPrinter.PrintRow(inMemoryBad);
ConsoleReportPrinter.PrintRow(inMemoryGood);
ConsoleReportPrinter.PrintSummary(inMemoryBad, inMemoryGood);

System.Console.WriteLine();

// SQLite
var dbPath = SqliteDatabaseFactory.GetDatabasePath("nplusone-roundtrips.db");
var sqliteDataSource = new SqliteRecordDataSource(dbPath, totalRecords);
var sqliteRunner = new ScenarioRunner(sqliteDataSource);

var sqliteBad = sqliteRunner.RunBad();
var sqliteGood = sqliteRunner.RunGood();

ConsoleReportPrinter.PrintHeader("SQLITE (DAPPER) - N+1 e Round Trips");
ConsoleReportPrinter.PrintRow(sqliteBad);
ConsoleReportPrinter.PrintRow(sqliteGood);
ConsoleReportPrinter.PrintSummary(sqliteBad, sqliteGood);

ConsoleReportPrinter.WaitForExit();