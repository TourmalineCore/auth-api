using System.Net;
using InnerCircle.Authentication.Service.Services;
using InnerCircle.Authentication.Service.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace InnerCircle.Authentication.Service.Controllers
{
    [Route("api/auth")]
    public class UsersController : Controller
    {
        private readonly UsersService _usersService;

        private const int CreatedStatusCode = (int)HttpStatusCode.Created;
        private const int InternalServerErrorCode = (int)HttpStatusCode.InternalServerError;

        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUserAsync([FromBody] RegistrationModel registrationModel)
        {
            try
            {
                await _usersService.RegisterAsync(registrationModel);
                return StatusCode(CreatedStatusCode);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, InternalServerErrorCode);
            }
        }

        [HttpPost("reset")]
        public Task RegisterUser([FromQuery] string corporateEmail)
        {
            return _usersService.ResetPasswordAsync(corporateEmail);
        }

        [HttpPut("change-password")]
        public async Task<ActionResult> ChangePasswordAsync([FromBody] PasswordChangeModel passwordChangeModel)
        {
            try
            {
                await _usersService.ChangePasswordAsync(passwordChangeModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, InternalServerErrorCode);
            }
        }
    }
}
