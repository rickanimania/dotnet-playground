using NPlusOneRoundTrips.Core.Services;
using NPlusOneRoundTrips.Console.Presentation;

const int totalRecords = 100;

var runner = new ScenarioRunner(totalRecords);

var bad = runner.RunBad();
var good = runner.RunGood();

ConsoleReportPrinter.PrintHeader("DOTNET-PLAYGROUND - Cenario conceitual de N+1 e Round Trips");

ConsoleReportPrinter.PrintRow(bad);
ConsoleReportPrinter.PrintRow(good);

ConsoleReportPrinter.PrintSummary(bad, good);

ConsoleReportPrinter.WaitForExit();