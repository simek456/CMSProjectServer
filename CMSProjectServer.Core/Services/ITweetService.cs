using CMSProjectServer.Domain;
using System.Text.Json;
using System.Threading.Tasks;

namespace CMSProjectServer.Core.Services;

public interface ITweetService
{
    Task<Result<JsonDocument>> GetTweet(string url);
}