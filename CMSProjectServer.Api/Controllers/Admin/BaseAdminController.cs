using CMSProjectServer.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMSProjectServer.Api.Controllers.Admin;

[ApiController]
[Authorize(Roles = UserRoles.Admin)]
public class BaseAdminController : ControllerBase
{
}