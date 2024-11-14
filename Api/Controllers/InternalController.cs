using Api.Services;
using Api.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("internal")]
public class InternalController : Controller
{
    private readonly UsersService _usersService;

    public InternalController(UsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpPost("block-user")]
    public async Task<ActionResult> BlockAsync([FromBody] BlockingRequest req)
    {
        try
        {
            await _usersService.BlockAsync(req.AccountId);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, nameof(InternalController), StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost("unblock-user")]
    public async Task<ActionResult> UnblockAsync([FromBody] UnblockingRequest req)
    {
        try
        {
            await _usersService.UnblockAsync(req.AccountId);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, nameof(InternalController), StatusCodes.Status500InternalServerError);
        }
    }
}