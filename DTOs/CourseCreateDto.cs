using System.ComponentModel.DataAnnotations;

namespace APDB_Kolokwium_template.DTOs;

public class CourseCreateDto
{
    [Required]
    [MaxLength(150)]
    public string Title { get; set; } = null!;
    
    [Required]
    [MaxLength(300)]
    public string Credits { get; set; } = null!;
    
    [Required]
    [MaxLength(150)]
    public string Teacher { get; set; } = null!;
    
    [Required]
    public ICollection<StudentCreateDto> Students { get; set; } = null!;
}

public class StudentCreateDto
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = null!;
    
    [Required]
    [MaxLength(150)]
    [EmailAddress]
    public string Email { get; set; } = null!;
} 