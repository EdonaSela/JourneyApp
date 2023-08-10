using JourneyApp.Journey.SignInManagement;
using JourneyApp.Models.Entities.Users;
using JourneyApp.Models.Exceptions.SignIn;
using Microsoft.AspNetCore.Identity;

namespace JourneyApp.Service.Foundations.SignIn
{
    public partial class SignInService : ISIgnInService
    {

        private readonly ISignInManagerJourney signInManagementJourney;

        public SignInService(ISignInManagerJourney signInManagementJourney)
        {
            this.signInManagementJourney = signInManagementJourney;
        }

        public async Task<SignInResult> CheckPasswordSignInAsync(
            User user,
            string password,
            bool lockoutOnFailure)
        {
            var result = await this.signInManagementJourney.CheckPasswordSignInAsync(
                user,
                password,
                lockoutOnFailure);

            ThrowExceptionIfFailed(result);

            return result;
        }

        private static void ThrowExceptionIfFailed(SignInResult result)
        {
            if (!result.Succeeded)
            {
                throw new InvalidSignInException();
            }

        }
    }
}
