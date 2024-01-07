using CMSProjectServer.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMSProjectServer.Core.Services;

public interface IAccountService
{
    Task<Result<bool>> AdminDeleteAccount(string callerUsername, string targetUsername);

    Task<Result<bool>> DeleteAccount(string username);

    Task<Result<List<string>>> GetAccountList(string? targetRole = null);
}