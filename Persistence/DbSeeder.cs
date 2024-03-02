using Domain;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure;

public class DbSeeder
{
    private readonly RoleManager<Role> _roleManager;

    public DbSeeder(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task Seed(AppDbContext context)
    {
        await SeedRoles(context);
    }

    private async Task SeedRoles(AppDbContext context)
    {
        if (!await _roleManager.RoleExistsAsync("Admin"))
        {
            await _roleManager.CreateAsync(new Role { Id = Guid.NewGuid(), Name = "Admin" });
        }

        if (!await _roleManager.RoleExistsAsync("User"))
        {
            await _roleManager.CreateAsync(new Role { Id = Guid.NewGuid(), Name = "User" });
        }
    }
}