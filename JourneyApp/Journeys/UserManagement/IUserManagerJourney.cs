using JourneyApp.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace JourneyApp.Journey.UserManagement
{
    public interface IUserManagementJourney
    {

        ValueTask<IdentityResult> InsertUserAsync(User user, string password);
        IQueryable<User> SelectAllUsers();
        ValueTask<User> SelectUserByUserNameAsync(string username);
        ValueTask<IdentityResult> UpdateUserAsync(User user);
        ValueTask<IdentityResult> AddToRoleAsync(User user, string roleName);
        ValueTask<IList<string>> GetUserRoles(User user);
    }
}
