using CMSProjectServer.Core.Services;
using CMSProjectServer.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CMSProjectServer.Api.Controllers;

[Authorize(Roles = UserRoles.User + "," + UserRoles.Admin)]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly IAccountService accountService;

    public AccountController(IAccountService accountService)
    {
        this.accountService = accountService;
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteMyAccount()
    {
        var username = User?.Identity?.Name;
        if (username is null)
        {
            return BadRequest("Unknown User");
        }
        var result = await accountService.DeleteAccount(username);
        if (result.IsSuccess)
        {
            return Ok();
        }
        return BadRequest(result.Error);
    }

    [HttpDelete("admin")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> DeleteMyAccount([FromQuery] string targetAccount)
    {
        var username = User?.Identity?.Name;
        if (username is null)
        {
            return BadRequest("Unknown User");
        }
        var result = await accountService.AdminDeleteAccount(username, targetAccount);
        if (result.IsSuccess)
        {
            return Ok();
        }
        return BadRequest(result.Error);
    }
}