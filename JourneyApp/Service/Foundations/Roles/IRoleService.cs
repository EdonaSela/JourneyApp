using JourneyApp.Models.DTOs.Roles;

namespace JourneyApp.Service.Foundations.Roles
{
    public interface IRoleService
    {
        ValueTask<RolesDto> AddRoleAsync(RolesDto roleDto);
        ValueTask<IEnumerable<RolesDto>> RetrieveAllRoles();
    }
}
