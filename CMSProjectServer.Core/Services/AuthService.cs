﻿using CMSProjectServer.Domain;
using CMSProjectServer.Domain.Dto;
using CMSProjectServer.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CMSProjectServer.Core.Services;

internal class AuthService : IAuthService
{
    private readonly UserManager<User> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<(int, string)> Registeration(UserRegistrationDto registrationDto, string role)
    {
        var userExists = await userManager.FindByNameAsync(registrationDto.Username);
        if (userExists != null)
        {
            return (0, "User already exists");
        }

        var user = new User()
        {
            Email = registrationDto.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = registrationDto.Username,
            Name = registrationDto.Name
        };
        var createUserResult = await userManager.CreateAsync(user, registrationDto.Password);
        if (!createUserResult.Succeeded)
        {
            return (0, "User creation failed! Please check user details and try again.");
        }

        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }

        if (await roleManager.RoleExistsAsync(UserRoles.User))
        {
            await userManager.AddToRoleAsync(user, role);
        }

        return (1, "User created successfully!");
    }

    public async Task<(int, string)> Login(UserLoginDto loginDto)
    {
        var user = await userManager.FindByNameAsync(loginDto.Username);

        if (user == null)
        {
            return (0, "Invalid username");
        }

        if (!await userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            return (0, "Invalid password");
        }

        var userRoles = await userManager.GetRolesAsync(user);
        var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.UserName),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }
        string token = GenerateToken(authClaims);
        return (1, token);
    }

    private string GenerateToken(IEnumerable<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _configuration["JWT:ValidIssuer"],
            Audience = _configuration["JWT:ValidAudience"],
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}