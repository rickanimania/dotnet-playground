using LayeredApiTemplate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LayeredApiTemplate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ExamplesController(IExampleService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await service.GetAllAsync(cancellationToken);
        return Ok(result);
    }
}