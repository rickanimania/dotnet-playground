using NPlusOneRoundTrips.Core.Abstractions;
using NPlusOneRoundTrips.Core.Models;

namespace NPlusOneRoundTrips.Core.Services;

public sealed class DataAccessSimulator : IRecordDataSource
{
    private readonly List<Record> _records;
    private readonly int _roundTripDelayMs;

    public DataAccessSimulator(int totalRecords, int roundTripDelayMs)
    {
        _roundTripDelayMs = roundTripDelayMs;
        _records = GenerateRecords(totalRecords);
    }

    private static List<Record> GenerateRecords(int total)
    {
        var list = new List<Record>(total);

        for (int i = 1; i <= total; i++)
        {
            list.Add(new Record
            {
                Id = i,
                ExternalKey = $"KEY-{i:D5}"
            });
        }

        return list;
    }

    public IEnumerable<Record> GetAll()
    {
        SimulateRoundTrip();
        return _records;
    }

    public Record? GetById(int id)
    {
        SimulateRoundTrip();
        return _records.FirstOrDefault(r => r.Id == id);
    }

    private void SimulateRoundTrip()
    {
        if (_roundTripDelayMs <= 0)
            return;

        Thread.Sleep(_roundTripDelayMs);
    }
}