using JourneyApp.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace JourneyApp.Journey.UserManagement
{
    public class UserManagementJourney : IUserManagementJourney
    {

        private readonly UserManager<User> userManagement;

        public UserManagementJourney(UserManager<User> userManagement)
        {
            this.userManagement = userManagement;
        }

        public async ValueTask<IdentityResult> InsertUserAsync(User user, string password)
        {
            var ident = new UserManagementJourney(this.userManagement);
            return await ident.userManagement.CreateAsync(user, password);
        }

        public IQueryable<User> SelectAllUsers()
        {
            var ident = new UserManagementJourney(this.userManagement);

            return ident.userManagement.Users;
        }

        public async ValueTask<User> SelectUserByUserNameAsync(string username)
        {
            var ident = new UserManagementJourney(this.userManagement);

            return await ident.userManagement.FindByNameAsync(username);
        }

        public async ValueTask<IdentityResult> UpdateUserAsync(User user)
        {
            var ident = new UserManagementJourney(this.userManagement);
            return await ident.userManagement.UpdateAsync(user);
        }

        public async ValueTask<IdentityResult> AddToRoleAsync(User user, string roleName)
        {
            var ident = new UserManagementJourney(this.userManagement);

            return await ident.userManagement.AddToRoleAsync(user, roleName);
        }

        public async ValueTask<IList<string>> GetUserRoles(User user)
        {
            var ident = new UserManagementJourney(this.userManagement);

            return await ident.userManagement.GetRolesAsync(user);
        }
    }
}
