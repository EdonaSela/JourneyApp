using JourneyApp.Infrastructure.Mappings;

using JourneyApp.Infrastructure.Providers.Tokens;
using JourneyApp.Models.Configurations.Authorizations;
using JourneyApp.Models.DTOs.Login;
using JourneyApp.Models.DTOs.Register;
using JourneyApp.Models.DTOs.journeys;
using JourneyApp.Models.DTOs.Users;
using JourneyApp.Models.Entities.journeys;
using JourneyApp.Models.Exceptions.Users;
using JourneyApp.Service.Foundations.SignIn;
using JourneyApp.Service.Foundations.Journeys;
using JourneyApp.Service.Foundations.Users;
using JourneyApp.Models.DTOs.Logout;
using JourneyApp.Models.Entities.Users;
using JourneyApp.Models.Exceptions.journeys;
using System.Text.RegularExpressions;

namespace JourneyApp.Service.Processings.Accounts
{

    public partial class AccountService : IAccountService
    {
        private readonly ISIgnInService signInService;
        private readonly IUSerService userService;
        private readonly IJourneyService journeyService;
        private readonly ITokenProvider tokenProvider;

        public AccountService(
            ISIgnInService signInService,
            IUSerService userService,
            ITokenProvider tokenProvider,
            IJourneyService journeyService)
        {
            this.signInService = signInService;
            this.userService = userService;
            this.tokenProvider = tokenProvider;
            this.journeyService = journeyService;
        }

        public async Task<UserDto> UserLoginAsync(LoginDto loginDto)
        {
            ValidateLoginDto(loginDto);

            var user = await this.userService.RetreiveUserByUserNameAsync(
                loginDto.UserName);

            ValidateStorageUserName(user, loginDto.UserName);

            await this.signInService.CheckPasswordSignInAsync(user, loginDto.Password, false);

            var userRole = await this.userService.RetreiveUserRoleAsync(user);

            var journey = new JourneyDto();

            

            string token = tokenProvider.CreateToken(user, userRole);

            return user.ToDto(token);
        }

        public async Task<UserDto> UserRegisterAsync(RegisterDto registerDto)
        {
            ValidateRegisterDto(registerDto);
            var storageUser =
                await this.userService.RetreiveUserByUserNameAsync(registerDto.UserName);

            ValidateExistsStorageUser(storageUser, registerDto.UserName);
            var newUser = registerDto.ToEntity();

            var user =
                await this.userService.RegisterUserAsync(newUser, registerDto.Password);

            string userRole = !string.IsNullOrWhiteSpace(registerDto.Role)
                ? await this.userService.AddToRoleAsync(user, registerDto.Role)
                : string.Empty;

            var journey = new JourneyDto();


            string token = tokenProvider.CreateToken(user, userRole);

            return user.ToDto(token);
        }
        public async Task<string> UserLogoutAsync(LogoutDto logoutDto)
        {
            //deactivate the access token
            try
            {
                await tokenProvider.DeactivateCurrentAsync();
                return "logout";
            }
            catch (Exception ex)
            {
                throw new InvalidUserException();
            }

        }



        private static void ValidateExistsStorageUser(User storageUser, string username)
        {
            if (storageUser is not null)
            {
                throw new AlreadyExistsUsernameException(username);
            }
        }

        private static void ValidateStorageUserName(User storageUser, string username)
        {
            if (storageUser == null)
            {
                throw new NotFoundUsernameException(username);
            }
        }

       

        private static void ValidateRegisterDto(RegisterDto registerDto)
        {
            if (registerDto is null)
            {
                throw new NullUserException();
            }

            Validate(
               (Rule: IsInvalid(registerDto.UserName, nameof(RegisterDto.UserName)), Parameter: nameof(RegisterDto.UserName)),
               (Rule: IsInvalidPassword(registerDto.Password), Parameter: nameof(RegisterDto.Password)),
               (Rule: IsInvalid(registerDto.FirstName, nameof(RegisterDto.FirstName)), Parameter: nameof(RegisterDto.FirstName)),
               (Rule: IsInvalid(registerDto.LastName, nameof(RegisterDto.LastName)), Parameter: nameof(RegisterDto.LastName))
               );
        }

        private static void ValidateLoginDto(LoginDto loginDto)
        {
            if (loginDto is null)
            {
                throw new NullUserException();
            }

            Validate(
               (Rule: IsInvalid(loginDto.UserName, nameof(LoginDto.UserName)), Parameter: nameof(LoginDto.UserName)),
               (Rule: IsInvalid(loginDto.Password, nameof(LoginDto.Password)), Parameter: nameof(LoginDto.Password))

               );
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidUserException = new InvalidUserException();

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
