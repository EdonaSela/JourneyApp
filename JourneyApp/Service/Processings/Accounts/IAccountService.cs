using JourneyApp.Models.DTOs.Login;
using JourneyApp.Models.DTOs.Logout;
using JourneyApp.Models.DTOs.Register;
using JourneyApp.Models.DTOs.Users;


namespace JourneyApp.Service.Processings.Accounts
{
    public interface IAccountService
    {
        Task<UserDto> UserLoginAsync(LoginDto loginDto);
        Task<UserDto> UserRegisterAsync(RegisterDto registerDto);
        Task<string> UserLogoutAsync(LogoutDto logoutDto);

    }
}
