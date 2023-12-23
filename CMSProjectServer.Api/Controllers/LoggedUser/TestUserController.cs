using Microsoft.AspNetCore.Mvc;

namespace CMSProjectServer.Api.Controllers.LoggedUser;

[Route("api/test")]
public class TestUserController : LoggedUserController
{
    [HttpGet("secret")]
    public IActionResult Secret()
    {
        return Ok("secret");
    }
}