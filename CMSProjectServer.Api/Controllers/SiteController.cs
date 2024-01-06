using CMSProjectServer.Core.Services;
using CMSProjectServer.Domain;
using CMSProjectServer.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSProjectServer.Api.Controllers;

[Route("api/site")]
[AllowAnonymous]
public class SiteController : ControllerBase
{
    private readonly ISiteService siteService;

    public SiteController(ISiteService siteService)
    {
        this.siteService = siteService;
    }

    [HttpGet("{siteId}")]
    public async Task<IActionResult> GetSite([FromRoute] string siteId)
    {
        var result = await siteService.GetSite(siteId);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return NotFound();
    }

    [HttpPost("{siteId}")]
    public async Task<IActionResult> SaveSite([FromRoute] string siteId, [FromBody] SiteDto siteDto)
    {
        var username = User?.Identity?.Name;
        if (username is null)
        {
            return BadRequest("Unknown User");
        }
        await siteService.AddSite(siteDto, siteId, username);
        return NoContent();
    }
}