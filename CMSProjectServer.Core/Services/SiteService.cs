﻿using AutoMapper;
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
        siteEntity.CreatedAt = DateTime.UtcNow;

        var oldSite = await dbContext.CurrentSites.FirstOrDefaultAsync(x => x.Name == siteId);
        if (oldSite != null)
        {
            siteEntity.UpdatedAt = DateTime.UtcNow;
            siteEntity.CreatedAt = oldSite.CreatedAt;
            var maxId = await dbContext.HistoricSites.Select(x => x.Id).MaxAsync(x => x);
            oldSite.Id = maxId + 1;
            dbContext.HistoricSites.Add(mapper.Map<OldSite>(oldSite));
            dbContext.CurrentSites.Remove(oldSite);
        }

        dbContext.CurrentSites.Add(siteEntity);
        await dbContext.SaveChangesAsync();
    }
}