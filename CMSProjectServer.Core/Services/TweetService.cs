using CMSProjectServer.Domain;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CMSProjectServer.Core.Services;

internal class TweetService : ITweetService
{
    private readonly HttpClient httpClient;

    public TweetService()
    {
        var handler = new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.All
        };
        httpClient = new HttpClient(handler);
    }

    public async Task<Result<JObject>> GetTweet(string url)
    {
        using HttpResponseMessage response = await httpClient.GetAsync($"https://publish.twitter.com/oembed?url={url}");
        if (response.IsSuccessStatusCode)
        {
            return Result<JObject>.Failure("Failed to retrieve tweet");
        }
        var contents = await response.Content.ReadAsStringAsync();
        return JObject.Parse(contents);
    }
}