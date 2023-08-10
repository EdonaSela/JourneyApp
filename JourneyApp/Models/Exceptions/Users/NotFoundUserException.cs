

namespace JourneyApp.Models.Exceptions.Users
{
    public class NotFoundUserException : Exception
    {

      

        public NotFoundUserException(string username)
       : base($"Couldn't find user with username: { username }.")
        {
        }

    }
}
