using DotnetPlayground.Common.ConsoleUI;
using DotnetPlayground.Common.ConsoleUI.Reports;
using DeferredExecutionMaterialization.Console.Configuration;
using DeferredExecutionMaterialization.Core.Models;
using DeferredExecutionMaterialization.Core.Services;
using DeferredExecutionMaterialization.Infrastructure.InMemory.DataSources;

const RunMode mode = RunMode.Demo;

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

ConsoleReportPrinter.WaitForExit();