using CMSProjectServer.Core.Services;
using CMSProjectServer.Domain;
using CMSProjectServer.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CMSProjectServer.Api.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthService authService;
    private readonly ILogger<AuthenticationController> logger;

    public AuthenticationController(IAuthService authService, ILogger<AuthenticationController> logger)
    {
        this.authService = authService;
        this.logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto loginDto)
    {
        try
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest("InvalidPayload");
            }
            var (status, message) = await authService.Login(loginDto);
            if (status == 0)
            {
                return BadRequest(message);
            }
            return Ok(message);
        }
        catch (System.Exception e)
        {
            logger.LogError(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost("registration")]
    public async Task<IActionResult> Register(UserRegistrationDto model)
    {
        try
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest("Invalid payload");
            }

            var (status, message) = await authService.Registeration(model, UserRoles.User);
            if (status == 0)
            {
                return BadRequest(message);
            }
            return CreatedAtAction(nameof(Register), model);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost("registration/admin")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> RegisterAdmin(UserRegistrationDto model)
    {
        try
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest("Invalid payload");
            }

            var (status, message) = await authService.Registeration(model, UserRoles.Admin);
            if (status == 0)
            {
                return BadRequest(message);
            }
            return CreatedAtAction(nameof(Register), model);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}