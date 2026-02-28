using Dapper;
using Microsoft.Data.Sqlite;
using NPlusOneRoundTrips.Core.Abstractions;
using NPlusOneRoundTrips.Core.Models;
using NPlusOneRoundTrips.Infrastructure.Sqlite.Database;

namespace NPlusOneRoundTrips.Infrastructure.Sqlite.DataSources;

public sealed class SqliteRecordDataSource : IRecordDataSource
{
    private readonly string _databasePath;

    public SqliteRecordDataSource(string databasePath, int totalRecordsToSeed)
    {
        _databasePath = databasePath;

        using var connection = SqliteDatabaseFactory.CreateConnection(_databasePath);
        SqliteDatabaseInitializer.EnsureCreatedAndSeeded(connection, totalRecordsToSeed);
    }

    public IEnumerable<Record> GetAll()
    {
        using var connection = SqliteDatabaseFactory.CreateConnection(_databasePath);
        connection.Open();

        const string sql = "SELECT id AS Id, externalKey AS ExternalKey FROM records ORDER BY id;";
        return connection.Query<Record>(sql).ToList();
    }

    public Record? GetById(int id)
    {
        using var connection = SqliteDatabaseFactory.CreateConnection(_databasePath);
        connection.Open();

        const string sql = "SELECT id AS Id, externalKey AS ExternalKey FROM records WHERE id = @id;";
        return connection.QueryFirstOrDefault<Record>(sql, new { id });
    }
}