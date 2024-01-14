using CMSProjectServer.Core.Services;
using CMSProjectServer.Domain;
using CMSProjectServer.Domain.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CMSProjectServer.Api.Controllers;

[Authorize(Roles = UserRoles.User + "," + UserRoles.Admin)]
[Route("api/comment")]
public class CommentController : ControllerBase
{
    private readonly ICommentService commentService;

    public CommentController(ICommentService commentService)
    {
        this.commentService = commentService;
    }

    [HttpGet("{articleId}")]
    public async Task<IActionResult> GetArticleComments([FromRoute] int articleId)
    {
        var result = await commentService.GetCommentsForArticle(articleId);
        return Ok(result);
    }

    [HttpPost("{articleId}")]
    public async Task<IActionResult> AddComment([FromBody] CommentDto commentDto, [FromRoute] int articleId)
    {
        string? username = null;
        var auth = await HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
        if (auth.Succeeded)
        {
            var claimsPrincipal = auth.Principal;
            username = claimsPrincipal?.Identity?.Name;
        }
        if (username == null)
        {
            return BadRequest("Unknown user");
        }
        var result = await commentService.AddComment(articleId, username, commentDto);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }

    [HttpPut("{articleId}")]
    public async Task<IActionResult> EditComment([FromBody] CommentDto commentDto, [FromRoute] int articleId)
    {
        string? username = null;
        var auth = await HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
        if (auth.Succeeded)
        {
            var claimsPrincipal = auth.Principal;
            username = claimsPrincipal?.Identity?.Name;
        }
        if (username == null)
        {
            return BadRequest("Unknown user");
        }
        var result = await commentService.EditComment(articleId, username, commentDto);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }

    [HttpDelete("{articleId}")]
    public async Task<IActionResult> DeleteComment([FromBody] CommentDto commentDto, [FromRoute] int articleId)
    {
        string? username = null;
        var auth = await HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
        if (auth.Succeeded)
        {
            var claimsPrincipal = auth.Principal;
            username = claimsPrincipal?.Identity?.Name;
        }
        if (username == null)
        {
            return BadRequest("Unknown user");
        }
        var result = await commentService.RemoveComment(articleId, username);
        if (result)
        {
            return Ok();
        }
        return BadRequest("You cannot delete this comment");
    }

    [Authorize(Roles = UserRoles.Admin)]
    [HttpDelete("{articleId}/admin")]
    public async Task<IActionResult> DeleteCommentAdmin([FromBody] CommentDto commentDto, [FromRoute] int articleId)
    {
        string? username = null;
        var auth = await HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
        if (auth.Succeeded)
        {
            var claimsPrincipal = auth.Principal;
            username = claimsPrincipal?.Identity?.Name;
        }
        if (username == null)
        {
            return BadRequest("Unknown user");
        }
        var result = await commentService.RemoveComment(articleId, username, true);
        if (result)
        {
            return Ok();
        }
        return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete this comment");
    }
}