using JourneyApp.Infrastructure.Mappings;
using JourneyApp.Journey.RoleManagement;
using JourneyApp.Models.DTOs.Roles;
using JourneyApp.Models.Exceptions.Roles;
using JourneyApp.Models.Exceptions.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JourneyApp.Service.Foundations.Roles
{
    public partial class RoleService : IRoleService
    {
        private readonly IRoleManagementJourney roleManagementJourney;

        public RoleService(IRoleManagementJourney roleManagementJourney)
        {
            this.roleManagementJourney = roleManagementJourney;
        }

        public async ValueTask<RolesDto> AddRoleAsync(RolesDto roleDto)
        {
            ValidateRoleOnCreate(roleDto);

            var role = roleDto.ToEntity();

            var result = await roleManagementJourney.InsertRoleAsync(role);

            ThrowExceptionIfAnyError(result);

            return roleDto;
        }

        public async ValueTask<IEnumerable<RolesDto>> RetrieveAllRoles()
        {
            var roleDtos = new List<RolesDto>();

            var roles =
                await roleManagementJourney.SelectAllRoles().ToListAsync();

            roleDtos.AddRange(roles.Select(role => role.ToDto()));

            return roleDtos;
        }

        public static void ValidateRoleOnCreate(RolesDto role)
        {
            if (role == null)
            {
                throw new NullRoleException();
            }

            if (string.IsNullOrWhiteSpace(role.Name))
            {
                throw new InvalidRoleException(nameof(role.Name), role.Name);
            }
        }

        private static void ThrowExceptionIfAnyError(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                var invalidUserException = new InvalidRoleException();

                foreach (var error in result.Errors)
                {
                    

                    throw new InvalidUserException(
                        error.Code,
                        error.Description);
                }

               
            }

        }
    }
}
