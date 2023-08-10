using JourneyApp.Infrastructure.Mappings;
using JourneyApp.Models.DTOs.journeys;
using JourneyApp.Models.DTOs.Journeys;
using JourneyApp.Models.Entities.journeys;
using JourneyApp.Models.Entities.Users;
using JourneyApp.Models.Exceptions.journeys;
using JourneyApp.Models.Exceptions.Users;
using JourneyApp.Service.Foundations.Journeys;
using JourneyApp.Service.Foundations.Users;
using Microsoft.EntityFrameworkCore;

namespace JourneyApp.Service.Processings.journeys
{
    public partial class JourneyProcessingService : IJourneyProccessingService
    {
        private readonly IJourneyService journeyService;
        private readonly IUSerService userService;

        public JourneyProcessingService(IJourneyService journeyService, IUSerService userService)
        {
            this.journeyService = journeyService;
            this.userService = userService;
        }

        public ValueTask<List<JourneyDto>> RetrieveAllJourneys(Guid search)
        {
            var journeys = this.journeyService.RetrieveUserJourneyAsync(search);

            return journeys;
        }
        public ValueTask<List<JourneyDto>> FilterJourneys(FilterDto filter)
        {
            var journeys = this.journeyService.FilterJourneys(filter);

            return journeys;
        }
        public ValueTask<List<JourneyRouteFilterDto>> MonthlyRoute(JourneyMonthlyRouteDto filter)
        {
            var journeys = this.journeyService.MonthlyRoute(filter);

            return journeys;
        }


        public async ValueTask<JourneyDto> CreateJourneys(JourneyDto journey)
        {
            var storagejourney = await this.journeyService.CreateJourneyAsync(journey);
            ValidateStoragejourney(storagejourney, journey.UserId);

            return storagejourney;
        }
        public async ValueTask<Boolean> DailyAchievement(Guid UserId)
        {
            var achievement =  await this.journeyService.DailyAchievement(UserId);

            return achievement;
        }
        
        public async ValueTask<Boolean> DeleteJourneys(Guid Id)
        {
            var deleted =  await this.journeyService.DeleteJourneyAsync(Id);

            return deleted;
        }

        private static void ValidateStoragejourney(JourneyDto storagejourney, Guid id)
        {
            if (storagejourney == null)
            {
                throw new NotFoundjourneyException(id);
            }
        }

    }
}
