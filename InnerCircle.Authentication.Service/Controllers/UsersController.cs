﻿using InnerCircle.Authentication.Service.Services;
using InnerCircle.Authentication.Service.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace InnerCircle.Authentication.Service.Controllers
{
    [Route("api")]
    public class UsersController : Controller
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("register")]
        public Task RegisterUser([FromBody] RegistrationModel requestModel)
        {
            return _usersService.RegisterAsync(requestModel);
        }
        
        [HttpPost("create-password")]
        public Task CreatePassword([FromBody] CreatePasswordModel requestModel)
        {
            return _usersService.CreatePasswordAsync(requestModel);
        }
    }
}