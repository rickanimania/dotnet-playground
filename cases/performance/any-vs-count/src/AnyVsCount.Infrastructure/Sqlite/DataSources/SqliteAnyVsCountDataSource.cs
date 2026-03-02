using AnyVsCount.Core.Abstractions;
using Dapper;
using Microsoft.Data.Sqlite;

namespace AnyVsCount.Infrastructure.Sqlite.DataSources;

public sealed class SqliteAnyVsCountDataSource : IExistenceDataSource
{
    private readonly string _dbPath;
    private readonly int _totalRecords;

    public SqliteAnyVsCountDataSource(string dbPath, int totalRecords)
    {
        _dbPath = dbPath;
        _totalRecords = totalRecords;
    }

    public int TotalRecords => _totalRecords;

    // No SQLite não tentamos medir "avaliados" (plano decide). Foco é tempo.
    public int BadEvaluatedRecords => 0;
    public int GoodEvaluatedRecords => 0;

    public void ExecuteBad()
    {
        using var connection = CreateConnection();
        // COUNT força agregação; mesmo com índice, é trabalho a mais do que "existir"
        _ = connection.ExecuteScalar<int>(
            "SELECT COUNT(1) FROM Records WHERE IsMatch = 1;");
    }

    public void ExecuteGood()
    {
        using var connection = CreateConnection();
        // LIMIT 1 para no primeiro match
        _ = connection.ExecuteScalar<int?>(
            "SELECT 1 FROM Records WHERE IsMatch = 1 LIMIT 1;");
    }

    private SqliteConnection CreateConnection()
    {
        var connection = new SqliteConnection($"Data Source={_dbPath}");
        connection.Open();
        return connection;
    }
}