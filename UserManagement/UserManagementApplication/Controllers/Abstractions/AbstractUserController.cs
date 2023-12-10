using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserManagement.Controllers.Dtos;
using UserManagementService.Common;

namespace UserManagement.Controllers.Interfaces
{
    [ApiController]
    [Route("users")]
    public abstract class AbstractUserController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "ADMIN, CLIENT")]
        [ProducesResponseType(typeof(Response<IEnumerable<UserDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<IEnumerable<UserDto>>), (int)HttpStatusCode.NotFound)]
        public abstract Task<IActionResult> FindAll();

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Response<UserDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<UserDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Response<UserDto>), (int)HttpStatusCode.Conflict)]
        public abstract Task<IActionResult> Add([FromBody] RegisterUserDto userDto);

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Response<UserDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<UserDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Response<UserDto>), (int)HttpStatusCode.Conflict)]
        public abstract Task<IActionResult> Update([FromBody] UserDto userDto, [FromRoute] Guid id);

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.NotFound)]
        public abstract Task<IActionResult> Delete([FromRoute] Guid id);
    }
}
