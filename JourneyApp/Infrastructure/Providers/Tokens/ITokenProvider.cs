using JourneyApp.Models.DTOs.journeys;
using JourneyApp.Models.Entities.Users;

namespace JourneyApp.Infrastructure.Providers.Tokens
{
    public interface ITokenProvider
    {

        string CreateToken(User user, string role);
        Task DeactivateCurrentAsync();
        Task<bool> IsActiveAsync(string token);
        Task DeactivateAsync(string token);
        Task<bool> IsCurrentActiveToken();

    }
}
