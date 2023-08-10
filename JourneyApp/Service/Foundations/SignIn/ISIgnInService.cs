using JourneyApp.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace JourneyApp.Service.Foundations.SignIn
{
    public interface ISIgnInService
    {

        Task<SignInResult> CheckPasswordSignInAsync(User user, string password, bool lockoutOnFailure);
    }
}
