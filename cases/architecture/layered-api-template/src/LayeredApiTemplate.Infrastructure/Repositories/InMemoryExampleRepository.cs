using LayeredApiTemplate.Domain.Entities;
using LayeredApiTemplate.Domain.Interfaces;

namespace LayeredApiTemplate.Infrastructure.Repositories;

public sealed class InMemoryExampleRepository : IExampleRepository
{
    public Task<IReadOnlyList<ExampleEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<ExampleEntity> data =
        [
            new() { Id = Guid.NewGuid(), Name = "Layered API Template" },
            new() { Id = Guid.NewGuid(), Name = "Separation of Concerns" }
        ];

        return Task.FromResult(data);
    }
}