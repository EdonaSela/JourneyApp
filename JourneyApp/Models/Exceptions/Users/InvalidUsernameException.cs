

namespace JourneyApp.Models.Exceptions.Users
{
    public class InvalidUserException : Exception
    {

        public InvalidUserException(string parameterName, object parameterValue)
          : base($"Invalid data, parameter name: {parameterName},parameter value: {parameterValue}.")
        {
        }

        public InvalidUserException()
         : base($"Invalid data. Please fix the errors and try again.")
        {
        }
       
            
    }
}
