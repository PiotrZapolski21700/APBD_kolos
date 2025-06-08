using APDB_Kolokwium_template.DTOs;

namespace APDB_Kolokwium_template.Services;

public interface IDbService
{
    public Task<ICollection<ExampleGetDto>> GetAllExamplesAsync();
    public Task<ICollection<OtherGetDto>> GetAllOthersAsync();
}