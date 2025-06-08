using APDB_Kolokwium_template.DTOs;
using APDB_Kolokwium_template.Services;
using Microsoft.AspNetCore.Mvc;

namespace APDB_Kolokwium_template.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentController : ControllerBase
{
    private readonly IDbService _dbService;

    public EnrollmentController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<EnrollmentGetDto>>> GetEnrollments()
    {
        var enrollments = await _dbService.GetEnrollmentsAsync();
        return Ok(enrollments);
    }
}