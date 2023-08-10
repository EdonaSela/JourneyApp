using JourneyApp.Infrastructure.Filters.Attributes.Claims;
using Microsoft.AspNetCore.Mvc;

namespace JourneyApp.Infrastructure.Filters.Attributes.Claims
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {

        public ClaimsAuthorizeAttribute(string claim)
            : base(typeof(ClaimsAuthorizationFilter))
        {
            Arguments = new object[] { claim };
        }
    }
}
