using CMSProjectServer.Domain;
using CMSProjectServer.Domain.Dto;
using System.Threading.Tasks;

namespace CMSProjectServer.Core.Services;

public interface ISiteService
{
    Task AddSite(SiteDto siteDto, string siteId, string username);

    Task<Result<SiteDto>> GetSite(string siteId);
}