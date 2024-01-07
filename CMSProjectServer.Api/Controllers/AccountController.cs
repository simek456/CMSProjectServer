using CMSProjectServer.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMSProjectServer.Api.Controllers;

[Authorize(Roles = UserRoles.User + "," + UserRoles.Admin)]
[Route("api/account")]
public class AccountController : ControllerBase
{
}