
namespace JourneyApp.Models.Exceptions.SignIn
{
    public class InvalidSignInException : Exception
    {

        public InvalidSignInException()
         : base($"Invalid password")
        {
        }
        public InvalidSignInException(string message)
         : base(message)
        {
        }


        
        
    }
}
