using Microsoft.Data.Sqlite;

namespace NPlusOneRoundTrips.Infrastructure.Sqlite.Database;

public static class SqliteDatabaseFactory
{
    public static string GetDatabasePath(string databaseFileName)
    {
        var baseDir = AppContext.BaseDirectory;

        // Sobe diretórios até a raiz do repo pode variar; por simplicidade:
        // cria o banco na pasta de execução em ".local".
        // Se você preferir na raiz do repo, ajustamos depois.
        var localDir = Path.Combine(baseDir, ".local");
        Directory.CreateDirectory(localDir);

        return Path.Combine(localDir, databaseFileName);
    }

    public static SqliteConnection CreateConnection(string databasePath)
    {
        var cs = new SqliteConnectionStringBuilder
        {
            DataSource = databasePath,
            Mode = SqliteOpenMode.ReadWriteCreate,
            Cache = SqliteCacheMode.Shared
        }.ToString();

        return new SqliteConnection(cs);
    }
}