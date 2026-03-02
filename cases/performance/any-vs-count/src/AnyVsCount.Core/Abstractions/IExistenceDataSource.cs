namespace AnyVsCount.Core.Abstractions;

public interface IExistenceDataSource
{
    int TotalRecords { get; }

    /// <summary>
    /// Quantidade de itens efetivamente avaliados no cenário ruim (quando aplicável).
    /// Para SQLite, pode ficar 0 e a evidência principal ser o tempo.
    /// </summary>
    int BadEvaluatedRecords { get; }

    /// <summary>
    /// Quantidade de itens efetivamente avaliados no cenário bom (quando aplicável).
    /// Para SQLite, pode ficar 0 e a evidência principal ser o tempo.
    /// </summary>
    int GoodEvaluatedRecords { get; }

    void ExecuteBad();  // Count() > 0
    void ExecuteGood(); // Any()
}