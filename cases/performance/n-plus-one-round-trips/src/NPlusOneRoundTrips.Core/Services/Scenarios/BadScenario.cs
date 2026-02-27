using NPlusOneRoundTrips.Core.Models;

namespace NPlusOneRoundTrips.Core.Services.Scenarios;

public sealed class BadScenario
{
    private readonly DataAccessSimulator _dataAccess;

    public BadScenario(DataAccessSimulator dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public List<Record> Execute()
    {
        // Materialização precoce: traz tudo para memória imediatamente
        var records = _dataAccess.GetAllRecords().ToList();

        var result = new List<Record>(records.Count);

        // N+1 + round-trips: consulta item a item dentro do loop
        foreach (var record in records)
        {
            var fullRecord = _dataAccess.GetRecordById(record.Id);
            if (fullRecord is not null)
                result.Add(fullRecord);
        }

        return result;
    }
}