
using JourneyApp.Journey.UserManagement;
using JourneyApp.Models.Entities.Users;
using JourneyApp.Models.Exceptions.Users;

using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace JourneyApp.Service.Foundations.Users
{


    public partial class UserService : IUSerService
    {
        private readonly IUserManagementJourney userManagementJourney;

        public UserService(IUserManagementJourney userManagementJourney)
        {
            this.userManagementJourney = userManagementJourney;
        }

        public async ValueTask<User> RegisterUserAsync(User user, string password)
        {
            ValidateUserOnCreate(user, password);
            var result = await this.userManagementJourney.InsertUserAsync(user, password);

            ThrowExceptionIfAnyError(result);

            return user;
        }

        public async ValueTask<User> RetreiveUserByUserNameAsync(string username) =>
            await this.userManagementJourney.SelectUserByUserNameAsync(username.ToLower());

        public async ValueTask<User> UpdateUserAsync(User user)
        {
            ValidateUserOnModify(user);
            User storageUser = await this.userManagementJourney.SelectUserByUserNameAsync(user.UserName);
            ValidateStorageUser(storageUser, user.UserName);

            var result = await this.userManagementJourney.UpdateUserAsync(user);
            ThrowExceptionIfAnyError(result);

            return user;
        }

        public async ValueTask<string> RetreiveUserRoleAsync(User user)
        {
            User storageUser = await this.userManagementJourney.SelectUserByUserNameAsync(user.UserName);
            ValidateStorageUser(storageUser, user.UserName);

            var roles = await this.userManagementJourney.GetUserRoles(user);

            return roles.Any() ? roles.First() : string.Empty;
        }
        public IQueryable<User> RetreiveUsersAsync()
        {
            var storageUser = this.userManagementJourney.SelectAllUsers();

            return storageUser;
        }

        public async ValueTask<string> AddToRoleAsync(User user, string roleName)
        {
            var result = await this.userManagementJourney.AddToRoleAsync(user, roleName);
            ThrowExceptionIfAnyError(result);

            return roleName;
        }
    }
    public partial class UserService
    {
        private static void ValidateUserOnCreate(User user, string password)
        {
            ValidateUserIsNull(user);

            Validate(
                (Rule: IsInvalid(user.UserName, nameof(User.UserName)), Parameter: nameof(User.UserName)),
                (Rule: IsInvalidPassword(password), Parameter: nameof(password)),
                (Rule: IsInvalid(user.FirstName, nameof(User.FirstName)), Parameter: nameof(User.FirstName)),
                (Rule: IsInvalid(user.LastName, nameof(User.LastName)), Parameter: nameof(User.LastName))
                );
        }

        private static void ValidateUserIsNull(User user)
        {
            if (user is null)
            {
                throw new NullUserException();
            }
        }

        private static void ThrowExceptionIfAnyError(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                

                foreach (var error in result.Errors)
                {
                  throw new InvalidUserException(
                       error.Code,
                       error.Description);

                    
                }

         
            }

        }

        private static void ValidateStorageUser(User storageUser, string username)
        {
            if (storageUser == null)
            {
                throw new NotFoundUserException(username);
            }
        }

        private static void ValidateUserOnModify(User user)
        {
            ValidateUserIsNull(user);

            Validate(
                (Rule: IsInvalid(user.Id, nameof(User.Id)), Parameter: nameof(User.Id)),
                (Rule: IsInvalid(user.UserName, nameof(User.UserName)), Parameter: nameof(User.UserName)),
                (Rule: IsInvalid(user.FirstName, nameof(User.FirstName)), Parameter: nameof(User.FirstName)),
                (Rule: IsInvalid(user.LastName, nameof(User.LastName)), Parameter: nameof(User.LastName))
                );
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            
            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    throw new InvalidUserException(
                        parameter,
                        rule.Message);
                }
            }

        
        }

        private static dynamic IsInvalid(string value, string fieldName) => new
        {
            Condition = string.IsNullOrWhiteSpace(value),
            Message = $"{fieldName} is required"
        };

        private static dynamic IsInvalid(Guid value, string fieldName) => new
        {
            Condition = value == Guid.Empty,
            Message = $"{fieldName} is required"
        };

        private static dynamic IsInvalidPassword(string password) => new
        {
            Condition = InvalidPassword(password),
            Message =
                $"Invalid password! " +
                $"Password must be at least 8 characters long " +
                $"and contain at least one number, one lowercase, " +
                $"one uppercase and one special character."
        };

        private static bool InvalidPassword(string password)
        {
            Regex rgx = new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$");

            if (string.IsNullOrWhiteSpace(password) || !rgx.IsMatch(password))
            {
                return true;
            }

            return false;
        }

    }
}


    
