using JourneyApp.Infrastructure.Filters.Attributes.Roles;
using JourneyApp.Models.Configurations.Authorizations;
using JourneyApp.Models.DTOs.journeys;
using JourneyApp.Models.DTOs.Journeys;
using JourneyApp.Models.Entities.journeys;
using JourneyApp.Service.Processings.journeys;
using Microsoft.AspNetCore.Mvc;

namespace JourneyApp.Controllers
{
   
        
        public class JourneyController : BaseApiController
        {
            private readonly IJourneyProccessingService journeyProcessingService;

            public JourneyController(IJourneyProccessingService journeyProcessingService)
            {
                this.journeyProcessingService = journeyProcessingService;
            }

            [HttpGet("id")]
            public async ValueTask<ActionResult<IEnumerable<JourneyDto>>> Getjourneys(Guid UserId) =>
                 Ok(await this.journeyProcessingService.RetrieveAllJourneys(UserId));

            [HttpPost("journey")]
            public async ValueTask<ActionResult<JourneyDto>> CreateJourney(JourneyDto journey) =>
                Ok(await this.journeyProcessingService.CreateJourneys(journey));

            [HttpPost("UserId")]
            public ActionResult<Boolean> DailyAchievement(Guid UserId) =>
                    Ok(this.journeyProcessingService.DailyAchievement(UserId));
        [HttpPost("DeletedId")]
            public  ActionResult<Boolean> DeleteJourney(Guid Id) =>
                Ok(this.journeyProcessingService.DeleteJourneys(Id));

            [HttpGet("filterJourney")]
            [RoleAuthorize(roleName: AplicationConsts.AdminRoleName)]
             public async ValueTask<ActionResult<List<JourneyDto>>> Filterjourneys(FilterDto filters) =>
                     Ok(await this.journeyProcessingService.FilterJourneys(filters));
            [HttpGet("filterRoute")]
            [RoleAuthorize(roleName: AplicationConsts.AdminRoleName)]
            public async ValueTask<ActionResult<List<JourneyRouteFilterDto>>> MonthlyRoute(JourneyMonthlyRouteDto filter) =>
                         Ok(await this.journeyProcessingService.MonthlyRoute(filter));
    }
}
