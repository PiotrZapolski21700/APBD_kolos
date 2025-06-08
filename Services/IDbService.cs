using APDB_Kolokwium_template.DTOs;

namespace APDB_Kolokwium_template.Services;

public interface IDbService
{
    Task<ICollection<EnrollmentGetDto>> GetEnrollmentsAsync();
    Task<CourseCreatedResponseDto> CreateCourseWithEnrollmentsAsync(CourseCreateDto courseData);
}