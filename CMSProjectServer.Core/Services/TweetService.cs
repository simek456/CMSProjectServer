using CMSProjectServer.Domain;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CMSProjectServer.Core.Services;

internal class TweetService : ITweetService
{
    private readonly HttpClient httpClient;

    public TweetService()
    {
        var handler = new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.All,
        };
        httpClient = new HttpClient(handler);
    }

    public async Task<Result<JsonDocument>> GetTweet(string url)
    {
        using HttpResponseMessage response = await httpClient.GetAsync($"https://publish.twitter.com/oembed?url={url}");
        if (response.IsSuccessStatusCode == false)
        {
            return Result<JsonDocument>.Failure("Failed to retrieve tweet");
        }
        var contents = await response.Content.ReadAsStringAsync();
        return JsonDocument.Parse(contents);
    }
}