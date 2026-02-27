using NPlusOneRoundTrips.Core.Models;

namespace NPlusOneRoundTrips.Core.Services;

public sealed class DataAccessSimulator
{
    private readonly List<Record> _records;

    public DataAccessSimulator(int totalRecords)
    {
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

    public IEnumerable<Record> GetAllRecords()
    {
        SimulateRoundTrip();
        return _records;
    }

    public Record? GetRecordById(int id)
    {
        SimulateRoundTrip();
        return _records.FirstOrDefault(r => r.Id == id);
    }

    private static void SimulateRoundTrip()
    {
        Thread.Sleep(1); // simula latência de banco
    }
}