using APDB_Kolokwium_template.Models;

namespace APDB_Kolokwium_template.DTOs;

public class OtherGetDto
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public ICollection<ExampleGetDto> Examples { get; set; } = null!;
}