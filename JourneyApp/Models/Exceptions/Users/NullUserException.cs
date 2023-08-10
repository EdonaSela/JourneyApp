

namespace JourneyApp.Models.Exceptions.Users
{
    public class NullUserException : Exception
    {

       
        public NullUserException()
           : base($"The user is null.")
        {
        }
    }
}
