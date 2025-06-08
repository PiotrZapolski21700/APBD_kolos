namespace APDB_Kolokwium_template.DTOs;

public class EnrollmentGetDto
{
    public StudentDto Student { get; set; } = null!;
    public CourseDto Course { get; set; } = null!;
    public DateTime EnrollmentDate { get; set; }
}

public class StudentDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
}

public class CourseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Teacher { get; set; } = null!;
} 