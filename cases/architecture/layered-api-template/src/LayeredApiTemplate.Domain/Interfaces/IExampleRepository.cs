using LayeredApiTemplate.Domain.Entities;

namespace LayeredApiTemplate.Domain.Interfaces;

public interface IExampleRepository
{
    Task<IReadOnlyList<ExampleEntity>> GetAllAsync(CancellationToken cancellationToken);
}