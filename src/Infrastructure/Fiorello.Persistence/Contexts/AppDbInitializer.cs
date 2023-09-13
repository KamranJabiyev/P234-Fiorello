using Fiorello.Domain.Entities;
using Fiorello.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Fiorello.Persistence.Contexts;

public class AppDbInitializer
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _config;

    public AppDbInitializer(AppDbContext context,
                            UserManager<AppUser> userManager,
                            RoleManager<IdentityRole> roleManager,
                            IConfiguration config)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _config = config;
    }

    public async Task UserSeedAsync()
    {
        AppUser appUser = new()
        {
            Email = _config["Admin:Email"],
            UserName = _config["Admin:UserName"]
        };
        await _userManager.CreateAsync(appUser, _config["Admin:Password"]);
        await _userManager.AddToRoleAsync(appUser,Roles.SuperAdmin.ToString());
    }
    public async Task CreateRoleAsync()
    {
        foreach(var role in Enum.GetValues(typeof(Roles)))
        {
            if(!await _roleManager.RoleExistsAsync(role.ToString()))
            {
                await _roleManager.CreateAsync(new()
                {
                    Name = role.ToString()
                });
            }
        }
    }

    public async Task IntializerAsync()
    {
        await _context.Database.MigrateAsync();
    }
}
