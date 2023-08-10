using JourneyApp.Models.Entities.Users;

namespace JourneyApp.Service.Foundations.Users
{
    public interface IUSerService
    {

        ValueTask<User> RegisterUserAsync(User user, string password);
        ValueTask<User> RetreiveUserByUserNameAsync(string username);
        ValueTask<User> UpdateUserAsync(User user);
        ValueTask<string> AddToRoleAsync(User user, string roleName);
        ValueTask<string> RetreiveUserRoleAsync(User user);
        IQueryable<User> RetreiveUsersAsync();
    }
}
