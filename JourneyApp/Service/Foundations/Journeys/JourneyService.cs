using JourneyApp.Journey.Storages;
using JourneyApp.Models.DTOs.journeys;
using JourneyApp.Models.DTOs.Journeys;
using JourneyApp.Models.Entities.journeys;
using JourneyApp.Models.Exceptions.journeys;
using JourneyApp.Models.Exceptions.Users;
using JourneyApp.Service.Foundations.Journeys;

namespace JourneyApp.Service.Foundations.Journey
{
    public  class JourneyService : IJourneyService
    {

        private readonly IStorageJourney storageJourney;

        public JourneyService(IStorageJourney storageJourney)
        {
            this.storageJourney = storageJourney;
        }

        public async ValueTask<JourneyDto> CreateJourneyAsync(JourneyDto journey)
        {
           
            ValidatejourneyIsNull(journey);

            return await this.storageJourney.InsertjourneyAsync(journey);
        }
        public async ValueTask<Boolean> DailyAchievement(Guid UserId)
        {
            return  await this.storageJourney.UpdateDailyAchievement(UserId);
        }
        
        public async ValueTask<Boolean> DeleteJourneyAsync(Guid Id)
        {
           var isDeleted= await this.storageJourney.DeletejourneyAsync(Id);
            return isDeleted;
        }

        public IQueryable<JourneyDto> RetrieveAllJourneys() =>
            this.storageJourney.SelectAlljourneys();

        public async ValueTask<List<JourneyDto>> RetrieveUserJourneyAsync(Guid userId)
        {
            var storagejourney = new List<JourneyDto>();
            storagejourney = await this.storageJourney.SelectjourneyByUserIdAsync(userId);
            ValidateStoragejourney(storagejourney, userId);

            return storagejourney;
        }
        public async ValueTask<List<JourneyDto>> FilterJourneys(FilterDto filter)
        {
            var storagejourney = await this.storageJourney.FilterJourneys(filter);

            return storagejourney;
        }
        public async ValueTask<List<JourneyRouteFilterDto>> MonthlyRoute(JourneyMonthlyRouteDto filter)
        {
            var storagejourney = await this.storageJourney.MonthlyRoute(filter);

            return storagejourney;
        }

        private static void ValidatejourneyOnCreate(JourneyDto journey)
        {
            ValidatejourneyIsNull(journey);

            Validate(
                (Rule: IsInvalid(journey.UserId, nameof(journey.UserId)), Parameter: nameof(journey.UserId)),
                (Rule: IsInvalid(journey.StartLocation.ToString(), nameof(journey.StartLocation)), Parameter: nameof(journey.StartLocation)),
                (Rule: IsInvalid(journey.StartTime.ToString(), nameof(journey.StartTime)), Parameter: nameof(journey.StartTime)),
                (Rule: IsInvalid(journey.transportationType.Id, nameof(journey.transportationType.Id)), Parameter: nameof(journey.transportationType.Id))
                );
        }

        private static void ValidatejourneyIsNull(JourneyDto journey)
        {
            if (journey is null)
            {
                throw new NulljourneyException();
            }
        }

        private static void ValidateStoragejourney(List<JourneyDto> storagejourney, Guid id)
        {
            if (storagejourney == null)
            {
                throw new NotFoundjourneyException(id);
            }
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    throw new InvalidUserException(
                        parameter,
                        rule.Message);
                }
            }

            
        }

        private static dynamic IsInvalid(string value, string fieldName) => new
        {
            Condition = string.IsNullOrWhiteSpace(value),
            Message = $"{fieldName} is required"
        };

        private static dynamic IsInvalid(Guid value, string fieldName) => new
        {
            Condition = value == Guid.Empty,
            Message = $"{fieldName} is required"
        };

        private static dynamic IsInvalid(DateTimeOffset value, string fieldName) => new
        {
            Condition = value == default,
            Message = $"{fieldName} is required"
        };

    
    }
}
