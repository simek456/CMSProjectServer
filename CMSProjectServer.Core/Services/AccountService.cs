﻿using CMSProjectServer.DAL;
using CMSProjectServer.Domain;
using CMSProjectServer.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSProjectServer.Core.Services;

internal class AccountService : IAccountService
{
    private readonly UserManager<User> userManager;
    private readonly CMSDbContext dbContext;

    public AccountService(UserManager<User> userManager, CMSDbContext dbContext)
    {
        this.userManager = userManager;
        this.dbContext = dbContext;
    }

    public async Task<Result<bool>> DeleteAccount(string username)
    {
        var user = await userManager.FindByNameAsync(username);
        if (user == null)
        {
            return true;
        }
        var role = await userManager.GetRolesAsync(user);
        if (role.Contains(UserRoles.Admin))
        {
            var admins = await userManager.GetUsersInRoleAsync(UserRoles.Admin);
            if (admins.Count == 1)
            {
                return Result<bool>.Failure("Can't delete last admin");
            }
        }
        var userInDb = await dbContext.Users.Include(x => x.Articles).Include(x => x.Likes).Include(x => x.EditedSites).Include(x => x.Comments).FirstAsync(x => x.UserName == username);
        userInDb.EditedSites.ForEach(x => x.ChangeAuthor = null);
        dbContext.Remove(userInDb);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<Result<bool>> AdminDeleteAccount(string callerUsername, string targetUsername)
    {
        if (callerUsername == targetUsername)
        {
            return Result<bool>.Failure("You can't delete your account this way");
        }
        return await DeleteAccount(targetUsername);
    }

    public async Task<Result<List<string>>> GetAccountList(string? targetRole = null)
    {
        if (targetRole == null)
        {
            return await dbContext.Users.Select(x => x.UserName).ToListAsync();
        }
        IList<User> users;
        switch (targetRole)
        {
            case UserRoles.Admin:
                users = await userManager.GetUsersInRoleAsync(UserRoles.Admin);
                return users.Select(x => x.UserName).ToList();

            case UserRoles.User:
                users = await userManager.GetUsersInRoleAsync(UserRoles.User);
                return users.Select(x => x.UserName).ToList();

            default:
                return Result<List<string>>.Failure("Unknown role");
        }
    }
}