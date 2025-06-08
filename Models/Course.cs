using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APDB_Kolokwium_template.Models;

[Table("Course")]
public class Course
{
    [Key]
    public int ID { get; set; }
    
    [MaxLength(150)]
    public string Title { get; set; } = null!;
    
    [MaxLength(300)]
    public string Credits { get; set; } = null!;
    
    [MaxLength(150)]
    public string Teacher { get; set; } = null!;
    
    public virtual ICollection<Enrollment> Enrollments { get; set; } = null!;
} 