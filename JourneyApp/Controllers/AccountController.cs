using JourneyApp.Models.DTOs.Login;
using JourneyApp.Models.DTOs.Logout;
using JourneyApp.Models.DTOs.Register;
using JourneyApp.Models.DTOs.Users;
using JourneyApp.Service.Processings.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace JourneyApp.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto) =>
            Ok(await this.accountService.UserRegisterAsync(registerDto));

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto) =>
            Ok(await this.accountService.UserLoginAsync(loginDto));

        [HttpPost("logout")]
        public async Task<ActionResult<string>> Logout(LogoutDto logoutDto) =>
             Ok(await this.accountService.UserLogoutAsync(logoutDto));
    }
}
