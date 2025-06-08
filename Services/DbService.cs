using APDB_Kolokwium_template.DAL;
using APDB_Kolokwium_template.DTOs;
using Microsoft.EntityFrameworkCore;

namespace APDB_Kolokwium_template.Services;

public class DbService(AppDbContext data) : IDbService
{
    public async Task<ICollection<ExampleGetDto>> GetAllExamplesAsync()
    {
        return await data.Examples.Select(e => new ExampleGetDto
        {
            Id = e.Id,
            Text = e.Text,
            OtherId = e.OtherId,
        }).ToListAsync();
    }

    public async Task<ICollection<OtherGetDto>> GetAllOthersAsync()
    {
        return await data.Others.Select(other => new OtherGetDto
        {
            Id = other.Id,
            Text = other.Text,
            Examples = other.Examples.Select(e => new ExampleGetDto
            {
                Id = e.Id,
                Text = e.Text,
                OtherId = e.OtherId
            }).ToList()
        }).ToListAsync();
    }
}