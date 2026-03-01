namespace DeferredExecutionMaterialization.Core.Abstractions;

public interface IMaterializationDataSource
{
    int TotalRecords { get; }

    /// <summary>
    /// Simula um fluxo onde o desenvolvedor materializa cedo (ToList) e depois filtra/processa em memória.
    /// </summary>
    void ExecuteBad();

    /// <summary>
    /// Simula um fluxo onde o desenvolvedor filtra antes e só materializa no final.
    /// </summary>
    void ExecuteGood();
}