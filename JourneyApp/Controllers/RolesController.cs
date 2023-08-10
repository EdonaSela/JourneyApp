using JourneyApp.Infrastructure.Filters.Attributes.Roles;
using JourneyApp.Service.Foundations.Roles;

using JourneyApp.Models.DTOs.Roles;
using Microsoft.AspNetCore.Mvc;
using JourneyApp.Models.Configurations.Authorizations;

namespace JourneyApp.Controllers
{
    [RoleAuthorize(roleName: AplicationConsts.AdminRoleName)]

    public class RolesController : BaseApiController
    {
        private readonly IRoleService roleService;

        public RolesController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<RolesDto>> CreateRole(RolesDto roleDto) =>
            Ok(await this.roleService.AddRoleAsync(roleDto));

        [HttpGet]
        public async ValueTask<ActionResult<IEnumerable<RolesDto>>> GetRoles()
        {
            return Ok(await this.roleService.RetrieveAllRoles());
        }
    }
}
