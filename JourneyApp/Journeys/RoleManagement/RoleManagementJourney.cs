using JourneyApp.Models.Entities.Roles;
using Microsoft.AspNetCore.Identity;

namespace JourneyApp.Journey.RoleManagement
{
    public class RoleManagementJourney : IRoleManagementJourney
    {

        private readonly RoleManager<Role> roleManagement;

        public RoleManagementJourney(RoleManager<Role> roleManagement)
        {
            this.roleManagement = roleManagement;
        }

        public async ValueTask<IdentityResult> InsertRoleAsync(Role role)
        {
            var ident = new RoleManagementJourney(this.roleManagement);

            return await ident.roleManagement.CreateAsync(role);
        }

        public IQueryable<Role> SelectAllRoles()
        {
            var ident = new RoleManagementJourney(this.roleManagement);

            return ident.roleManagement.Roles;
        }
    }
}
