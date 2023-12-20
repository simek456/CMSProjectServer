﻿using CMSProjectServer.Domain.Dto;
using System.Threading.Tasks;

namespace CMSProjectServer.Core.Services;
internal interface IAuthService
{
    Task<(int, string)> Login(UserLoginDto loginDto);
    Task<(int, string)> Registeration(UserRegistrationDto registrationDto, string role);
}