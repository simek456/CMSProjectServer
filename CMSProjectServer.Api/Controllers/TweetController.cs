using CMSProjectServer.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CMSProjectServer.Api.Controllers;

[Route("/api/tweet")]
public class TweetController : ControllerBase
{
    private readonly ITweetService tweetService;

    public TweetController(ITweetService tweetService)
    {
        this.tweetService = tweetService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTweet([FromQuery] string tweet)
    {
        var result = await tweetService.GetTweet(tweet);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
    }
}