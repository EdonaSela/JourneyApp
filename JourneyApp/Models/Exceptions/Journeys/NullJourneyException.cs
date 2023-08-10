

namespace JourneyApp.Models.Exceptions.journeys
{
    public class NulljourneyException :Exception
    {
      


        public NulljourneyException()
           : base($"The journey is null.")
        {
        }
    }
}
