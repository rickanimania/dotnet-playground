using NPlusOneRoundTrips.Core.Diagnostics;

namespace NPlusOneRoundTrips.Console.Presentation;

public static class ConsoleReportPrinter
{
    public static void PrintHeader(string title)
    {
        System.Console.WriteLine(title);
        System.Console.WriteLine();
        System.Console.WriteLine("|----------------|-----------|-----------|");
        System.Console.WriteLine("| CENARIO        | REGISTROS | TEMPO (MS)|");
        System.Console.WriteLine("|----------------|-----------|-----------|");
    }

    public static void PrintRow(ScenarioRunResult result)
    {
        var ms = ToMilliseconds(result.ElapsedTicks);

        System.Console.WriteLine(
            $"| {result.ScenarioName,-14} | {result.TotalRecords,9} | {ms,9:0.00} |");

        System.Console.WriteLine("|----------------|-----------|-----------|");
    }

    public static void PrintSummary(ScenarioRunResult bad, ScenarioRunResult good)
    {
        System.Console.WriteLine();

        if (bad.ElapsedTicks <= 0 || good.ElapsedTicks <= 0)
        {
            System.Console.WriteLine("Resumo: nao foi possivel calcular melhoria (tempo invalido).");
            return;
        }

        var ratio = (double)bad.ElapsedTicks / good.ElapsedTicks;
        var improvement = 100.0 - ((double)good.ElapsedTicks / bad.ElapsedTicks * 100.0);

        System.Console.WriteLine(
            $"Resumo: o cenario Good e {ratio:0.00}x mais rapido. Reducao aproximada de {improvement:0.0}%");
    }

    public static void WaitForExit()
    {
        System.Console.WriteLine();
        System.Console.WriteLine("Pressione ENTER para sair...");
        System.Console.ReadLine();
    }

    private static double ToMilliseconds(long ticks)
    {
        return (double)ticks * 1000.0 / System.Diagnostics.Stopwatch.Frequency;
    }
}