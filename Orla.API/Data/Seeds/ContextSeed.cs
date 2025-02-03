using Microsoft.AspNetCore.Identity;
using Orla.Api.Models;
using Orla.Core.Enums;

namespace Orla.Api.Data.Seeds;

public static class ContextSeed
{
    public static async Task SeedRolesAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        //Seed Roles
        await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));        
    }
}
