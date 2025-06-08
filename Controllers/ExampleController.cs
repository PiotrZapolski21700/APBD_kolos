using APDB_Kolokwium_template.Services;
using Microsoft.AspNetCore.Mvc;

namespace APDB_Kolokwium_template.Controllers;

[ApiController]
[Route("/v1/[controller]")]
public class ExampleController(IDbService service) : ControllerBase
{
    [HttpGet]
    [Route("/others")]
    public async Task<IActionResult> GetAllOthersAsync()
    {
        return Ok(await service.GetAllOthersAsync());
    }

    [HttpGet]
    [Route("/examples")]
    public async Task<IActionResult> GetAllExamplesAsync()
    {
        return Ok(await service.GetAllExamplesAsync());
    }
}