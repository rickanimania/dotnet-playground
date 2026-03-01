namespace DeferredExecutionMaterialization.Core.Models;

public sealed class CaseRunOptions
{
    public required RunMode Mode { get; init; }

    /// <summary>
    /// Quantidade total de itens a serem processados no cenário.
    /// </summary>
    public int Items { get; init; }

    /// <summary>
    /// Delay artificial (ms) para simular custo de acesso a dados.
    /// </summary>
    public int DelayMs { get; init; }

    public static CaseRunOptions From(RunMode mode)
        => mode switch
        {
            RunMode.Demo => new CaseRunOptions { Mode = mode, Items = 500, DelayMs = 2 },
            RunMode.Stress => new CaseRunOptions { Mode = mode, Items = 10_000, DelayMs = 0 },
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, "RunMode inválido.")
        };
}