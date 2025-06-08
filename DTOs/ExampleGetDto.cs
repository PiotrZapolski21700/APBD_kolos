using APDB_Kolokwium_template.Models;

namespace APDB_Kolokwium_template.DTOs;

public class ExampleGetDto
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public int OtherId { get; set; }
    
}