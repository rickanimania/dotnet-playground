using Microsoft.Data.Sqlite;

namespace AnyVsCount.Infrastructure.Sqlite.Database;

public static class SqliteDatabaseFactory
{
    public static string GetDatabasePath(string fileName)
    {
        var baseDir = AppContext.BaseDirectory;
        return Path.Combine(baseDir, fileName);
    }

    public static void EnsureDatabaseCreated(string dbPath, int totalRecords, int matchId = 10)
    {
        EnsureDirectory(dbPath);

        using var connection = new SqliteConnection($"Data Source={dbPath}");
        connection.Open();

        using var cmd = connection.CreateCommand();

        cmd.CommandText =
        """
        CREATE TABLE IF NOT EXISTS Records
        (
            Id      INTEGER PRIMARY KEY,
            IsMatch INTEGER NOT NULL
        );

        CREATE INDEX IF NOT EXISTS IX_Records_IsMatch ON Records(IsMatch);
        """;
        cmd.ExecuteNonQuery();

        // Se já tem dados, não recria
        cmd.CommandText = "SELECT COUNT(1) FROM Records;";
        var existing = Convert.ToInt32(cmd.ExecuteScalar());

        cmd.CommandText = "SELECT COUNT(1) FROM Records WHERE IsMatch = 1;";
        var matchCount = Convert.ToInt32(cmd.ExecuteScalar());

        cmd.CommandText = "SELECT Id FROM Records WHERE IsMatch = 1 LIMIT 1;";
        var matchIdInDb = cmd.ExecuteScalar();
        var matchIdOk = matchIdInDb is long id && id == matchId;

        if (existing == totalRecords && matchCount == 1 && matchIdOk)
            return;

        // Recria determinístico
        cmd.CommandText = "DELETE FROM Records;";
        cmd.ExecuteNonQuery();

        using var tx = connection.BeginTransaction();

        cmd.Transaction = tx;
        cmd.CommandText = "INSERT INTO Records (Id, IsMatch) VALUES ($id, $isMatch);";

        var idParam = cmd.CreateParameter();
        idParam.ParameterName = "$id";
        cmd.Parameters.Add(idParam);

        var isMatchParam = cmd.CreateParameter();
        isMatchParam.ParameterName = "$isMatch";
        cmd.Parameters.Add(isMatchParam);

        for (var i = 1; i <= totalRecords; i++)
        {
            idParam.Value = i;
            isMatchParam.Value = (i == matchId) ? 1 : 0;
            cmd.ExecuteNonQuery();
        }

        tx.Commit();
    }

    private static void EnsureDirectory(string dbPath)
    {
        var dir = Path.GetDirectoryName(dbPath);
        if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
            Directory.CreateDirectory(dir);
    }
}