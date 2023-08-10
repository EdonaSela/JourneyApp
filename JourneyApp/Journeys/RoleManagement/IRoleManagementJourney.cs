using JourneyApp.Models.Entities.Roles;
using Microsoft.AspNetCore.Identity;

namespace JourneyApp.Journey.RoleManagement
{
    public interface IRoleManagementJourney
    {
        ValueTask<IdentityResult> InsertRoleAsync(Role role);
        IQueryable<Role> SelectAllRoles();
    }
}
