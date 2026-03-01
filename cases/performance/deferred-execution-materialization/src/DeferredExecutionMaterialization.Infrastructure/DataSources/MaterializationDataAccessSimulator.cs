using DeferredExecutionMaterialization.Core.Abstractions;

namespace DeferredExecutionMaterialization.Infrastructure.InMemory.DataSources;

public sealed class MaterializationDataAccessSimulator : IMaterializationDataSource
{
    private readonly int _delayMs;

    public int TotalRecords { get; }

    public MaterializationDataAccessSimulator(int totalRecords, int delayMs)
    {
        if (totalRecords <= 0)
            throw new ArgumentOutOfRangeException(nameof(totalRecords));

        if (delayMs < 0)
            throw new ArgumentOutOfRangeException(nameof(delayMs));

        TotalRecords = totalRecords;
        _delayMs = delayMs;
    }

    public void ExecuteBad()
    {
        // Simula "ToList cedo": você paga custo de buscar tudo
        for (int i = 0; i < TotalRecords; i++)
            SimulateDataAccessCost();

        // Depois filtra/processa em memória (custo adicional)
        for (int i = 0; i < TotalRecords; i++)
            SimulateInMemoryCost();
    }

    public void ExecuteGood()
    {
        // Simula filtrar antes e materializar no final:
        // você não “busca tudo”, trabalha com um subconjunto
        var filtered = TotalRecords / 2;

        for (int i = 0; i < filtered; i++)
            SimulateDataAccessCost();

        // e processa menos em memória
        for (int i = 0; i < filtered; i++)
            SimulateInMemoryCost();
    }

    private void SimulateDataAccessCost()
    {
        if (_delayMs > 0)
            Thread.Sleep(_delayMs);
    }

    private static void SimulateInMemoryCost()
    {
        // Custo mínimo só para diferenciar fluxo.
        // Não coloque Sleep aqui, para não mascarar o custo de "buscar dados".
        _ = Guid.NewGuid();
    }
}