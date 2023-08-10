

namespace JourneyApp.Models.Exceptions.Roles;


public class InvalidRoleException :Exception
{

    public InvalidRoleException()
        : base($"Invalid role. Please fix the errors and try again.")
    {
    }
    public InvalidRoleException(string parameterName, object parameterValue)
       : base($"Invalid role, parameter name:{ parameterName}, + parameter value:{ parameterValue}.")
    {
    }

    
}
