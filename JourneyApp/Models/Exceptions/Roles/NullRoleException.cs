

namespace JourneyApp.Models.Exceptions.Roles
{
    public class NullRoleException : Exception

    {

        public NullRoleException()
        : base($"The role is null.")
        {
        }
        
    }
}
