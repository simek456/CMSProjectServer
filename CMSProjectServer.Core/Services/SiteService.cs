using AutoMapper;
using CMSProjectServer.DAL;
using CMSProjectServer.Domain;
using CMSProjectServer.Domain.Dto;
using CMSProjectServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSProjectServer.Core.Services;

internal class SiteService : ISiteService
{
    private readonly CMSDbContext dbContext;
    private readonly IMapper mapper;

    public SiteService(CMSDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<Result<SiteDto>> GetSite(string siteId)
    {
        var site = await dbContext.CurrentSites.FirstOrDefaultAsync(x => x.Name == siteId);
        if (site == null)
        {
            return Result<SiteDto>.Failure("Site not found");
        }
        return mapper.Map<SiteDto>(site);
    }

    public async Task AddSite(SiteDto siteDto, string siteId, string username)
    {
        var changeAuthor = await dbContext.Users.FirstOrDefaultAsync(x => x.UserName == username);
        if (changeAuthor == null)
        {
            return;
        }
        var siteEntity = mapper.Map<Site>(siteDto);
        siteEntity.ChangeAuthor = changeAuthor;
        siteEntity.Name = siteId;
        siteEntity.CreatedAt = DateTime.UtcNow;

        var oldSite = await dbContext.CurrentSites.FirstOrDefaultAsync(x => x.Name == siteId);
        if (oldSite != null)
        {
            siteEntity.UpdatedAt = DateTime.UtcNow;
            siteEntity.CreatedAt = oldSite.CreatedAt;
            await AddHistoricSite(oldSite);
            dbContext.CurrentSites.Remove(oldSite);
        }

        dbContext.CurrentSites.Add(siteEntity);
        await dbContext.SaveChangesAsync();
    }

    private async Task AddHistoricSite(Site oldSite)
    {
        dbContext.HistoricSites.Add(mapper.Map<OldSite>(oldSite));
    }

    public async Task<List<string>> GetAllSites()
    {
        var siteList = await dbContext.CurrentSites.Select(x => x.Name).ToListAsync();
        return siteList;
    }

    public async Task DeleteSite(string siteId)
    {
        var site = await dbContext.CurrentSites.FirstOrDefaultAsync(x => x.Name.Equals(siteId));
        if (site == null)
        {
            return;
        }
        await AddHistoricSite(site);
        dbContext.CurrentSites.Remove(site);
        await dbContext.SaveChangesAsync();
    }
}