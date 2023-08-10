
using JourneyApp.Models.Entities.journeys;


using JourneyApp.Models.DTOs.Error;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using JourneyApp.Models.Configurations.Authorizations;

namespace JourneyApp.Infrastructure.Filters.Attributes.Claims
{
    public class ClaimsAuthorizationFilter : IAuthorizationFilter
    {

        private readonly string _claimType;

        public ClaimsAuthorizationFilter(string claimType)
        {
            _claimType = claimType;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user is null
                || user.Identity is null
                || !user.Identity.IsAuthenticated)
            {
                context.Result = new ForbidResult();
                return;
            }

        }
    }
}
