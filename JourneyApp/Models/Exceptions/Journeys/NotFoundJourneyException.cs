

namespace JourneyApp.Models.Exceptions.journeys
{
    public class NotFoundjourneyException :Exception
    {

       

        public NotFoundjourneyException(Guid id)
           : base($"Journey with ID '{id}' not found.")
        {
        }
    }
}
