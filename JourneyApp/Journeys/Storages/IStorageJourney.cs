using JourneyApp.Models.DTOs.journeys;
using JourneyApp.Models.DTOs.Journeys;
using JourneyApp.Models.Entities.journeys;

namespace JourneyApp.Journey.Storages
{
    
    public interface IStorageJourney
    {
        ValueTask<JourneyDto> InsertjourneyAsync(JourneyDto journey);
        ValueTask<Boolean> UpdateDailyAchievement(Guid UserId);
        ValueTask<Boolean> DeletejourneyAsync(Guid Id);
        IQueryable<JourneyDto> SelectAlljourneys();
        ValueTask<JourneyDto> SelectjourneyByIdAsync(Guid journeyId);
        ValueTask<List<JourneyDto>> SelectjourneyByUserIdAsync(Guid userId);
        ValueTask<List<JourneyDto>> FilterJourneys(FilterDto filter);
        ValueTask<List<JourneyRouteFilterDto>> MonthlyRoute(JourneyMonthlyRouteDto filter);

    }
}
