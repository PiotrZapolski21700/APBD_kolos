using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APDB_Kolokwium_template.Models;

[Table("Enrollment")]
[PrimaryKey(nameof(Student_ID), nameof(Course_ID))]
public class Enrollment
{
    public int Student_ID { get; set; }
    
    public int Course_ID { get; set; }
    
    public DateTime EnrollmentDate { get; set; }
    
    [ForeignKey(nameof(Student_ID))]
    public virtual Student Student { get; set; } = null!;
    
    [ForeignKey(nameof(Course_ID))]
    public virtual Course Course { get; set; } = null!;
}