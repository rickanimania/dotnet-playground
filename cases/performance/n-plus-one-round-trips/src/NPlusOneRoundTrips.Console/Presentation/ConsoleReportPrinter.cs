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
        System.Console.WriteLine($"| {result.ScenarioName,-14} | {result.TotalRecords,9} | {result.ElapsedMilliseconds,9} |");
        System.Console.WriteLine("|----------------|-----------|-----------|");
    }

    public static void PrintSummary(ScenarioRunResult bad, ScenarioRunResult good)
    {
        System.Console.WriteLine();

        if (bad.ElapsedMilliseconds <= 0 || good.ElapsedMilliseconds <= 0)
        {
            System.Console.WriteLine("Resumo: nao foi possivel calcular melhoria (tempo invalido).");
            return;
        }

        var ratio = (double)bad.ElapsedMilliseconds / good.ElapsedMilliseconds;
        var improvement = 100.0 - ((double)good.ElapsedMilliseconds / bad.ElapsedMilliseconds * 100.0);

        System.Console.WriteLine($"Resumo: o cenario Good e {ratio:0.00}x mais rapido. Reducao aproximada de {improvement:0.0}%");
    }

    public static void WaitForExit()
    {
        System.Console.WriteLine();
        System.Console.WriteLine("Pressione ENTER para sair...");
        System.Console.ReadLine();
    }
}