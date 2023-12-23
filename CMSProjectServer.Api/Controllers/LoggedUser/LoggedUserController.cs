using CMSProjectServer.Api.Controllers.Admin;
using CMSProjectServer.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMSProjectServer.Api.Controllers.LoggedUser;

[ApiController]
[Authorize(Roles = UserRoles.User)]
public class LoggedUserController : BaseAdminController
{
}