using Dapper;
using Microsoft.Data.Sqlite;
using DeferredExecutionMaterialization.Core.Abstractions;

namespace DeferredExecutionMaterialization.Infrastructure.Sqlite.DataSources;

public sealed class SqliteMaterializationDataSource : IMaterializationDataSource
{
    private readonly string _connectionString;

    public int TotalRecords { get; }

    public int BadMaterializedRecords { get; private set; }
    public int GoodMaterializedRecords { get; private set; }

    public SqliteMaterializationDataSource(string dbPath, int totalRecords)
    {
        if (string.IsNullOrWhiteSpace(dbPath))
            throw new ArgumentException("Caminho do banco invalido.", nameof(dbPath));

        if (totalRecords <= 0)
            throw new ArgumentOutOfRangeException(nameof(totalRecords));

        _connectionString = $"Data Source={dbPath}";
        TotalRecords = totalRecords;
    }

    public void ExecuteBad()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        // Simula ToList cedo: SELECT * (materializa tudo)
        var all = connection.Query<RecordRow>("SELECT Id, IsActive FROM Records;").ToList();
        BadMaterializedRecords = all.Count;

        // Filtra em memória (pior caso)
        var filtered = all.Where(x => x.IsActive == 1).ToList();

        // Processa subset (só pra ter custo mínimo)
        Process(filtered);
    }

    public void ExecuteGood()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        // Simula filtro no servidor: SELECT ... WHERE IsActive = 1
        var filtered = connection.Query<RecordRow>("SELECT Id, IsActive FROM Records WHERE IsActive = 1;").ToList();
        GoodMaterializedRecords = filtered.Count;

        Process(filtered);
    }

    private static void Process(List<RecordRow> records)
    {
        long checksum = 0;
        foreach (var r in records)
            checksum += r.Id;

        _ = checksum;
    }

    private sealed class RecordRow
    {
        public int Id { get; set; }
        public int IsActive { get; set; }
    }
}