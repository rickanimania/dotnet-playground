using LayeredApiTemplate.Application.DTOs;
using LayeredApiTemplate.Application.Interfaces;
using LayeredApiTemplate.Domain.Interfaces;

namespace LayeredApiTemplate.Application.Services;

public sealed class ExampleService(IExampleRepository repository) : IExampleService
{
    public async Task<IReadOnlyList<ExampleResponseDTO>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(cancellationToken);

        return entities
            .Select(x => new ExampleResponseDTO { Id = x.Id, Name = x.Name })
            .ToList();
    }
}