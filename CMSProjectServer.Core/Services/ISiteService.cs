using CMSProjectServer.Domain;
using CMSProjectServer.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSProjectServer.Core.Services;

public interface ISiteService
{
    Task AddSite(SiteDto siteDto, string siteId, string username);

    Task DeleteSite(string siteId);

    Task<List<string>> GetAllSites();

    Task<Result<SiteDto>> GetSite(string siteId);
}