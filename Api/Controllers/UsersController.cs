using System.Net;
using Api.Services;
using Api.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Filters;

namespace Api.Controllers
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

        [Authorize]
        [RequiresPermission(UserClaimsProvider.CanManageAccounts)]
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

        [Authorize]
        [RequiresPermission(UserClaimsProvider.IsAccountsHardDeleteAllowed)]
        [HttpPost("delete-user")]
        public async Task<ActionResult> DeleteUserAsync([FromBody] DeletionModel deletionModel)
        {
            try
            {
                await _usersService.DeleteAsync(deletionModel);
                return StatusCode(CreatedStatusCode);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, InternalServerErrorCode);
            }
        }

        // Add new endpoint to create a user for local env protected with auth and new permission
        // CanCreateUserWithPasswordBypassingEmailConfirmation (can change)
        [Authorize]
        [RequiresPermission(UserClaimsProvider.CanSetUserPasswordBypassingEmailConfirmation)]
        [HttpPut("set-password")]
        public Task SetPassword([FromBody] PasswordSetModel passwordSetModel)
        {
            return _usersService.SetPasswordBypassingEmailConfirmationAsync(passwordSetModel);
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