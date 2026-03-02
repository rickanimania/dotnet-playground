using DeferredExecutionMaterialization.Core.Abstractions;

namespace DeferredExecutionMaterialization.Infrastructure.DataSources;

public sealed class MaterializationDataAccessSimulator : IMaterializationDataSource
{
    private readonly int _delayMsPerRecord;
    private readonly List<FakeRecord> _records;

    public int TotalRecords { get; }

    public int BadMaterializedRecords { get; private set; }
    public int GoodMaterializedRecords { get; private set; }

    public MaterializationDataAccessSimulator(int totalRecords, int delayMsPerRecord)
    {
        if (totalRecords <= 0)
            throw new ArgumentOutOfRangeException(nameof(totalRecords));

        if (delayMsPerRecord < 0)
            throw new ArgumentOutOfRangeException(nameof(delayMsPerRecord));

        TotalRecords = totalRecords;
        _delayMsPerRecord = delayMsPerRecord;

        // 25% ativos (simula filtro seletivo)
        _records = BuildRecords(totalRecords, activeRate: 0.25);
    }

    public void ExecuteBad()
    {
        // Simula: SELECT * FROM tabela;  (materializa tudo)
        var all = Materialize(_records);
        BadMaterializedRecords = all.Count;

        // Depois filtra em memória
        var filtered = all.Where(x => x.IsActive).ToList();

        // E processa o subset
        Process(filtered);
    }

    public void ExecuteGood()
    {
        // Simula: SELECT * FROM tabela WHERE IsActive = 1; (filtra "no servidor")
        var serverFiltered = _records.Where(x => x.IsActive).ToList();

        // Materializa apenas o subset
        var filtered = Materialize(serverFiltered);
        GoodMaterializedRecords = filtered.Count;

        // Processa o subset
        Process(filtered);
    }

    // ----------------- helpers -----------------

    private List<FakeRecord> Materialize(List<FakeRecord> source)
    {
        // custo por registro trafegado/materializado (rede + IO + parse)
        if (_delayMsPerRecord > 0)
        {
            // Evita Sleep em loop muito grande no Stress; mas no Demo é ok.
            // Como você tem Stress com delay 0, fica safe.
            foreach (var _ in source)
                Thread.Sleep(_delayMsPerRecord);
        }

        // Retorna cópia para simular materialização (novo objeto/lista)
        return source.Select(r => new FakeRecord(r.Id, r.IsActive)).ToList();
    }

    private static void Process(List<FakeRecord> records)
    {
        // custo de CPU/mem: simula transformação/validação
        // leve, mas proporcional ao volume
        long checksum = 0;
        foreach (var r in records)
            checksum += r.Id;

        _ = checksum;
    }

    private static List<FakeRecord> BuildRecords(int total, double activeRate)
    {
        var list = new List<FakeRecord>(total);
        var activeEvery = Math.Max((int)Math.Round(1 / activeRate), 1);

        for (int i = 1; i <= total; i++)
        {
            var isActive = (i % activeEvery) == 0;
            list.Add(new FakeRecord(i, isActive));
        }

        return list;
    }

    private sealed record FakeRecord(int Id, bool IsActive);
}