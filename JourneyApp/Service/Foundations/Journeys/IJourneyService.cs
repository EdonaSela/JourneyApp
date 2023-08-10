using JourneyApp.Models.DTOs.journeys;
using JourneyApp.Models.DTOs.Journeys;
using JourneyApp.Models.Entities.journeys;

namespace JourneyApp.Service.Foundations.Journeys
{
    public interface IJourneyService
    {

        ValueTask<JourneyDto> CreateJourneyAsync(JourneyDto journey);
        ValueTask<Boolean> DailyAchievement(Guid UserId);
        IQueryable<JourneyDto> RetrieveAllJourneys();
        ValueTask<List<JourneyDto>> RetrieveUserJourneyAsync(Guid userId);
        ValueTask<Boolean> DeleteJourneyAsync(Guid Id);
        ValueTask<List<JourneyDto>> FilterJourneys(FilterDto filter);
        ValueTask<List<JourneyRouteFilterDto>> MonthlyRoute(JourneyMonthlyRouteDto filter);

    }
}
