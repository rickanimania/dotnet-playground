using AnyVsCount.Core.Models;

namespace AnyVsCount.Console.Configuration;

public sealed class RunConfiguration
{
    public RunConfiguration(int totalRecords, int matchIndex, int workFactor)
    {
        TotalRecords = totalRecords;
        MatchIndex = matchIndex;
        WorkFactor = workFactor;
    }

    public int TotalRecords { get; }
    public int MatchIndex { get; }
    public int WorkFactor { get; }

    public static RunConfiguration GetConfig(RunMode mode)
    {
        // Match cedo (índice 10) para evidenciar parada antecipada do Any()
        const int matchIndex = 10;

        return mode switch
        {
            RunMode.Demo => new RunConfiguration(
                totalRecords: 500,
                matchIndex: matchIndex,
                workFactor: 200
            ),

            RunMode.Stress => new RunConfiguration(
                totalRecords: 100_000,
                matchIndex: matchIndex,
                workFactor: 50
            ),

            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, "RunMode inválido.")
        };
    }
}