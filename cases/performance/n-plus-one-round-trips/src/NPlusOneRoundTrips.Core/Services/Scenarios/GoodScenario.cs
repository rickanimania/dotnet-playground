using NPlusOneRoundTrips.Core.Models;

namespace NPlusOneRoundTrips.Core.Services.Scenarios;

public sealed class GoodScenario
{
    private readonly DataAccessSimulator _dataAccess;

    public GoodScenario(DataAccessSimulator dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public List<Record> Execute()
    {
        // Uma única ida ao "banco": sem N+1 e sem round-trips repetidos
        var records = _dataAccess.GetAllRecords();

        // Materializa apenas no final (quando realmente precisa da lista)
        return records.ToList();
    }
}