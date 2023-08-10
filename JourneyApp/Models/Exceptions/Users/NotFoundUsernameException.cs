

namespace JourneyApp.Models.Exceptions.Users
{
    public class NotFoundUsernameException : Exception
    {

       

        public NotFoundUsernameException(string username)
          : base($"Username {username} does not exist in the system.")
        {
        }

    }
}
