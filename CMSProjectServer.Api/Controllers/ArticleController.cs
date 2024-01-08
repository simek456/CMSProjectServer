using CMSProjectServer.Core.Services;
using CMSProjectServer.Domain;
using CMSProjectServer.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSProjectServer.Api.Controllers;

[AllowAnonymous]
[Route("api/article")]
public class ArticleController : ControllerBase
{
    private readonly IArticleService articleService;

    public ArticleController(IArticleService articleService)
    {
        this.articleService = articleService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetArticle([FromRoute] int id)
    {
        var result = await articleService.GetArticleById(id);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return NotFound();
    }

    [HttpGet("{id}/short")]
    public async Task<IActionResult> GetArticleShort([FromRoute] int id)
    {
        var result = await articleService.GetArticleShortById(id);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return NotFound();
    }

    [HttpPost()]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> CreateArticle([FromBody] ArticleDto articleDto)
    {
        var username = User?.Identity?.Name;
        if (username is null)
        {
            return BadRequest("Unknown User");
        }

        var result = await articleService.CreateArticle(articleDto, username);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> UpdateArticle([FromBody] ArticleDto articleDto, [FromRoute] int id)
    {
        var username = User?.Identity?.Name;
        if (username is null)
        {
            return BadRequest("Unknown User");
        }

        articleDto.Id = id;
        var result = await articleService.UpdateArticle(articleDto, username);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }

    [HttpDelete()]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> DeleteArticles([FromQuery] List<int> articleIds)
    {
        await articleService.DeleteArticles(articleIds);
        return Ok();
    }

    [HttpGet("id-title-map/{pageSize}")]
    public async Task<IActionResult> GetArticleListMap([FromRoute] int pageSize, [FromQuery] int? page, [FromQuery] int? categoryId, [FromQuery] string? order, [FromQuery] string? authorId)
    {
        if (!ValidSorting(order))
        {
            return BadRequest("Incorrect Sorting type");
        }

        var result = await articleService.GetArticleIdNameMap(pageSize, page, categoryId, order, authorId);
        return Ok(result);
    }

    [HttpGet("list/{pageSize}")]
    public async Task<IActionResult> GetArticleListShort([FromRoute] int pageSize, [FromQuery] int? page, [FromQuery] int? categoryId, [FromQuery] string? order, [FromQuery] string? authorId)
    {
        if (!ValidSorting(order))
        {
            return BadRequest("Incorrect Sorting type");
        }
        var result = await articleService.GetArticleListShort(pageSize, page, categoryId, order, authorId);
        return Ok(result);
    }

    private bool ValidSorting(string? sorting)
    {
        if (sorting == null)
        {
            return true;
        }
        return SortingType.AvailableSortings.Contains(sorting);
    }
}