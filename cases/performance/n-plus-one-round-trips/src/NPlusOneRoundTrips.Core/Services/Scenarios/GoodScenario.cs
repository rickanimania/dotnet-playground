using NPlusOneRoundTrips.Core.Abstractions;
using NPlusOneRoundTrips.Core.Models;

namespace NPlusOneRoundTrips.Core.Services.Scenarios;

public sealed class GoodScenario
{
    private readonly IRecordDataSource _dataSource;

    public GoodScenario(IRecordDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public List<Record> Execute()
    {
        // Uma única ida ao "banco": sem N+1 e sem round-trips repetidos
        var records = _dataSource.GetAll();

        // Materializa apenas no final (quando realmente precisa da lista)
        return records.ToList();
    }
}