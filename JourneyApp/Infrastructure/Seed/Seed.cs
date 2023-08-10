using JourneyApp.Models.Configurations.Authorizations;
using JourneyApp.Models.Entities.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JourneyApp.Infrastructure.Seed
{
    public class Seed
    {

        public static async Task SeedRoles(IHost host)
        {
            using var serviceScope = host.Services.CreateScope();

            using var roleManager = serviceScope.ServiceProvider
                .GetRequiredService<RoleManager<Role>>();

            if (await roleManager.Roles.AnyAsync()) return;

            var roles = new List<Role>
            {
                new Role{Name = AplicationConsts.AdminRoleName},
                new Role{Name = AplicationConsts.GuestRoleName},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

        }
    }
}
