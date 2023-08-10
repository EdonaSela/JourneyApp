using Microsoft.AspNetCore.Mvc;

namespace JourneyApp.Models.DTOs.Error
{
    public class ForbiddenErrorObjectResult : ObjectResult
    {
        public ForbiddenErrorObjectResult(object error)
            : base(error)
        {
            StatusCode = StatusCodes.Status403Forbidden;
        }
    }
}
