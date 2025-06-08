using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APDB_Kolokwium_template.Models;

[Table(nameof(Example))]
public class Example
{
    [Key]
    public int Id { get; set; }
    
    [Column(nameof(OtherId))]
    public int OtherId { get; set; }

    [ForeignKey(nameof(OtherId))] 
    public virtual Other Other { get; set; } = null!;
    
    [Required] [MaxLength(50)] public string Text { get; set; }
}