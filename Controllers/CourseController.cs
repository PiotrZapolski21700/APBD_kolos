using APDB_Kolokwium_template.DTOs;
using APDB_Kolokwium_template.Services;
using Microsoft.AspNetCore.Mvc;

namespace APDB_Kolokwium_template.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly IDbService _dbService;

    public CourseController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpPost("with-enrollments")]
    public async Task<ActionResult<CourseCreatedResponseDto>> CreateCourseWithEnrollments([FromBody] CourseCreateDto courseData)
    {
        var result = await _dbService.CreateCourseWithEnrollmentsAsync(courseData);
        return Ok(result);
    }
} 