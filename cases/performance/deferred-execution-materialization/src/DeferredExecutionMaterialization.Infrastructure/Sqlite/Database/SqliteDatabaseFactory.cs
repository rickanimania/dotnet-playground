using Microsoft.Data.Sqlite;

namespace DeferredExecutionMaterialization.Infrastructure.Sqlite.Database;

public static class SqliteDatabaseFactory
{
    public static string GetDatabasePath(string fileName)
    {
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        return Path.Combine(baseDir, fileName);
    }

    public static void EnsureDatabaseCreated(string dbPath, int totalRecords, double activeRate = 0.25)
    {
        if (File.Exists(dbPath))
            File.Delete(dbPath);

        using var connection = new SqliteConnection($"Data Source={dbPath}");
        connection.Open();

        using var cmd = connection.CreateCommand();

        cmd.CommandText = """
        CREATE TABLE Records (
            Id INTEGER PRIMARY KEY,
            IsActive INTEGER NOT NULL
        );
        """;
        cmd.ExecuteNonQuery();

        // Bulk insert simples (transação)
        using var tx = connection.BeginTransaction();
        cmd.Transaction = tx;

        cmd.CommandText = "INSERT INTO Records (Id, IsActive) VALUES ($id, $isActive);";

        var pId = cmd.CreateParameter();
        pId.ParameterName = "$id";
        cmd.Parameters.Add(pId);

        var pActive = cmd.CreateParameter();
        pActive.ParameterName = "$isActive";
        cmd.Parameters.Add(pActive);

        var activeEvery = Math.Max((int)Math.Round(1 / activeRate), 1);

        for (int i = 1; i <= totalRecords; i++)
        {
            var isActive = (i % activeEvery) == 0 ? 1 : 0;

            pId.Value = i;
            pActive.Value = isActive;

            cmd.ExecuteNonQuery();
        }

        tx.Commit();

        // Index para deixar o filtro mais real
        cmd.Transaction = null;
        cmd.Parameters.Clear();
        cmd.CommandText = "CREATE INDEX IX_Records_IsActive ON Records(IsActive);";
        cmd.ExecuteNonQuery();
    }
}