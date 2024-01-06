using CMSProjectServer.Core.Services;
using CMSProjectServer.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CMSProjectServer.Api.Controllers;

[Authorize(Roles = UserRoles.Admin)]
[Authorize(Roles = UserRoles.User)]
[Route("api/likes")]
public class LikeController : ControllerBase
{
    private readonly ILikeService likeService;

    public LikeController(ILikeService likeService)
    {
        this.likeService = likeService;
    }

    [HttpPost]
    public async Task<IActionResult> Like([FromQuery] int articleId)
    {
        var username = User?.Identity?.Name;
        if (username is null)
        {
            return BadRequest("Unknown User");
        }
        var result = await likeService.Like(username, articleId);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpDelete]
    public async Task<IActionResult> Dislike([FromQuery] int articleId)
    {
        var username = User?.Identity?.Name;
        if (username is null)
        {
            return BadRequest("Unknown User");
        }
        var result = await likeService.DislikeLike(username, articleId);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }
}