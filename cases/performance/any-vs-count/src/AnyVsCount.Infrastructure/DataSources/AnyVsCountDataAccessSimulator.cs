using AnyVsCount.Core.Abstractions;

namespace AnyVsCount.Infrastructure.DataSources;

public sealed class AnyVsCountDataAccessSimulator : IExistenceDataSource
{
    private readonly List<Record> _records;
    private readonly int _workFactor;

    public AnyVsCountDataAccessSimulator(int totalRecords, int matchIndex, int workFactor)
    {
        if (totalRecords <= 0)
            throw new ArgumentOutOfRangeException(nameof(totalRecords), "TotalRecords deve ser maior que zero.");

        if (matchIndex < 0 || matchIndex >= totalRecords)
            throw new ArgumentOutOfRangeException(nameof(matchIndex), "MatchIndex deve estar dentro do intervalo da coleção.");

        if (workFactor < 0)
            throw new ArgumentOutOfRangeException(nameof(workFactor), "WorkFactor não pode ser negativo.");

        _workFactor = workFactor;
        _records = BuildRecords(totalRecords, matchIndex);

        TotalRecords = totalRecords;
    }

    public int TotalRecords { get; }

    public int BadEvaluatedRecords { get; private set; }
    public int GoodEvaluatedRecords { get; private set; }

    public void ExecuteBad()
    {
        BadEvaluatedRecords = 0;

        // Simula o padrão ruim: Count(predicate) > 0
        // Isso força varredura total para obter a contagem.
        var count = 0;

        foreach (var record in _records)
        {
            BadEvaluatedRecords++;

            BurnCpu(_workFactor);

            if (record.IsMatch)
                count++;
        }

        _ = count > 0;
    }

    public void ExecuteGood()
    {
        GoodEvaluatedRecords = 0;

        // Simula o padrão bom: Any(predicate)
        // Para no primeiro match.
        var exists = false;

        foreach (var record in _records)
        {
            GoodEvaluatedRecords++;

            BurnCpu(_workFactor);

            if (record.IsMatch)
            {
                exists = true;
                break;
            }
        }

        _ = exists;
    }

    private static List<Record> BuildRecords(int totalRecords, int matchIndex)
    {
        var list = new List<Record>(capacity: totalRecords);

        for (var i = 0; i < totalRecords; i++)
        {
            list.Add(new Record
            {
                Id = i + 1,
                IsMatch = i == matchIndex
            });
        }

        return list;
    }

    private static void BurnCpu(int workFactor)
    {
        // workFactor = 0 => sem custo artificial
        // workFactor > 0 => custo leve e previsível por item, evitando "0ms" em demo
        // Não use Random aqui pra manter determinístico.
        unchecked
        {
            var acc = 0;
            for (var i = 0; i < workFactor; i++)
                acc = (acc * 31) + i;

            // impede otimização agressiva
            _ = acc;
        }
    }

    private sealed class Record
    {
        public int Id { get; init; }
        public bool IsMatch { get; init; }
    }
}