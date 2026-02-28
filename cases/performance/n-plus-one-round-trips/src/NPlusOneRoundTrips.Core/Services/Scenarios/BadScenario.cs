using NPlusOneRoundTrips.Core.Abstractions;
using NPlusOneRoundTrips.Core.Models;

namespace NPlusOneRoundTrips.Core.Services.Scenarios;

public sealed class BadScenario
{
    private readonly IRecordDataSource _dataSource;

    public BadScenario(IRecordDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public List<Record> Execute()
    {
        // Materialização precoce: traz tudo para memória imediatamente
        var records = _dataSource.GetAll().ToList();

        var result = new List<Record>(records.Count);

        // N+1 + round-trips: consulta item a item dentro do loop
        foreach (var record in records)
        {
            var fullRecord = _dataSource.GetById(record.Id);
            if (fullRecord is not null)
                result.Add(fullRecord);
        }

        return result;
    }
}