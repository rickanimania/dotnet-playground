using LayeredApiTemplate.Application.DTOs;

namespace LayeredApiTemplate.Application.Interfaces;

public interface IExampleService
{
    Task<IReadOnlyList<ExampleResponseDTO>> GetAllAsync(CancellationToken cancellationToken);
}