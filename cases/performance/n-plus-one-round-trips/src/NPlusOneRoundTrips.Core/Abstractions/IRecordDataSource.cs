using NPlusOneRoundTrips.Core.Models;

namespace NPlusOneRoundTrips.Core.Abstractions;

public interface IRecordDataSource
{
    IEnumerable<Record> GetAll();
    Record? GetById(int id);
}