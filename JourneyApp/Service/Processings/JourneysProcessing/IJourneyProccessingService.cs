using JourneyApp.Models.DTOs.journeys;
using JourneyApp.Models.DTOs.Journeys;
using JourneyApp.Models.Entities.journeys;

namespace JourneyApp.Service.Processings.journeys
{
    public interface IJourneyProccessingService
    {

        ValueTask<List<JourneyDto>> RetrieveAllJourneys(Guid search);
        ValueTask<JourneyDto> CreateJourneys(JourneyDto search);
        ValueTask<Boolean> DailyAchievement(Guid UserId);
        ValueTask<Boolean> DeleteJourneys(Guid Id);
        ValueTask<List<JourneyDto>> FilterJourneys(FilterDto filter);
        ValueTask<List<JourneyRouteFilterDto>> MonthlyRoute(JourneyMonthlyRouteDto monthNumber);

    }
}
