namespace JourneyApp.Models.Exceptions.Users
{
    public class AlreadyExistsUsernameException : AggregateException
    {

        public AlreadyExistsUsernameException(string username)
           : base($"Username '{username}' exists in the system. Please take another username.")
        {
        }
    }
}
