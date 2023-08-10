
namespace JourneyApp.Models.Exceptions.journeys
{
    public class InvalidJourneyException : Exception

    {
      

        public InvalidJourneyException(string parameterName, object parameterValue)
       : base($"Invalid journey, parameter name: {parameterName}, parameter value: { parameterValue}.")
        {
        }

    }
}
