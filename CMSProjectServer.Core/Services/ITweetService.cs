using CMSProjectServer.Domain;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace CMSProjectServer.Core.Services;

public interface ITweetService
{
    Task<Result<JObject>> GetTweet(string url);
}