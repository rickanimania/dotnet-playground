namespace DotnetPlayground.Common.ConsoleUI.Reports;

public sealed class ConsoleScenarioRunResult
{
    public required string ScenarioName { get; init; }

    // Mantém compatibilidade com os cases atuais
    public int TotalRecords { get; init; }

    public long ElapsedTicks { get; init; }

    // NOVO (opcional): tamanho total do dataset
    public int? DatasetRecords { get; init; }

    // NOVO (opcional): quantos foram efetivamente avaliados
    public int? EvaluatedRecords { get; init; }
}