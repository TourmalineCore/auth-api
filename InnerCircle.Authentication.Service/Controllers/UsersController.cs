using InnerCircle.Authentication.Service.Services;
using InnerCircle.Authentication.Service.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace InnerCircle.Authentication.Service.Controllers
{
    [Route("auth")]
    public class UsersController : Controller
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("register")]
        public Task RegisterUser([FromBody] RegistrationModel requestModel)
        {
            return _usersService.RegisterAsync(requestModel);
        }

        [HttpPost("reset")]
        public Task RegisterUser([FromQuery] string corporateEmail)
        {
            return _usersService.ResetPasswordAsync(corporateEmail);
        }

        [HttpPost("create-password")]
        public Task CreatePassword([FromBody] CreatePasswordModel requestModel)
        {
            return _usersService.CreatePasswordAsync(requestModel);
        }
    }
}
