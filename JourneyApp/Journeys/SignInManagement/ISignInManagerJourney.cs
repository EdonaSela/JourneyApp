using JourneyApp.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace JourneyApp.Journey.SignInManagement
{
    public interface ISignInManagerJourney
    {

        Task<SignInResult> CheckPasswordSignInAsync(
           User user,
           string password,
           bool lockoutOnFailure);
    }
}
