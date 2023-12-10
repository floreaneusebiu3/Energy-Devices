﻿using System.Web.Mvc;

namespace DevicesManagementConsole.Controllers.Abstractions
{
    [ApiController]
    [Route("authentication")]
    public abstract class AbstractAuthenticationController : ControllerBase
    {
        [HttpPost("login")]
        [ProducesResponseType(typeof(Response<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<string>), (int)HttpStatusCode.NotFound)]
        public abstract Task<IActionResult> Login(LoginDto loginDto);

        [HttpPost("register")]
        [ProducesResponseType(typeof(Response<UserDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<UserDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Response<UserDto>), (int)HttpStatusCode.Conflict)]
        public abstract Task<IActionResult> Register(RegisterUserDto registerUserDto);
    }
}