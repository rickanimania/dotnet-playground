using Dapper;
using Microsoft.Data.Sqlite;

namespace NPlusOneRoundTrips.Infrastructure.Sqlite.Database;

public static class SqliteDatabaseInitializer
{
    public static void EnsureCreatedAndSeeded(SqliteConnection connection, int totalRecords)
    {
        connection.Open();

        EnsureSchema(connection);
        EnsureSeed(connection, totalRecords);

        connection.Close();
    }

    private static void EnsureSchema(SqliteConnection connection)
    {
        const string sql = """
        CREATE TABLE IF NOT EXISTS records (
            id INTEGER PRIMARY KEY,
            externalKey TEXT NOT NULL
        );
        """;

        connection.Execute(sql);
    }

    private static void EnsureSeed(SqliteConnection connection, int totalRecords)
    {
        var currentCount = connection.ExecuteScalar<int>("SELECT COUNT(1) FROM records;");

        if (currentCount == totalRecords)
            return;

        // Mantém simples e determinístico: limpa e recria seed com a quantidade solicitada
        using var tx = connection.BeginTransaction();

        connection.Execute("DELETE FROM records;", transaction: tx);

        const string insertSql = "INSERT INTO records (id, externalKey) VALUES (@Id, @ExternalKey);";

        var rows = Enumerable.Range(1, totalRecords)
            .Select(i => new { Id = i, ExternalKey = $"KEY-{i:D5}" })
            .ToList();

        connection.Execute(insertSql, rows, transaction: tx);

        tx.Commit();
    }
}