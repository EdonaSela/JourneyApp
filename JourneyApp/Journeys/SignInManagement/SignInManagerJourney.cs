using JourneyApp.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace JourneyApp.Journey.SignInManagement
{
    public class SignInManagerJourney : ISignInManagerJourney
    {

        private readonly SignInManager<User> signInManagement;

        public SignInManagerJourney(SignInManager<User> signInManagement)
        {
            this.signInManagement = signInManagement;
        }

        public async Task<SignInResult> CheckPasswordSignInAsync(
            User user,
            string password,
            bool lockoutOnFailure)
        {
            return await this.signInManagement.CheckPasswordSignInAsync(
                user,
                password,
                lockoutOnFailure);
        }
    }
}
