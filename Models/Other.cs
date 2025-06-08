using System.ComponentModel.DataAnnotations;

namespace APDB_Kolokwium_template.Models;

public class Other
{
    [Key]
    public int Id { get; set; }

    [Required] [MaxLength(50)] public string Text { get; set; } = null!;

    public virtual ICollection<Example> Examples { get; set; } = null!;
}