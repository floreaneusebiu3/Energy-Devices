using DeviceManagementService.Common;
using DeviceManagementService.Mappers.UserMappers.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DeviceManagement.Controllers.Abstractions
{
    [ApiController]
    [Route("devices-users")]
    public abstract class AbstractUserController : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Response<string>), (int)HttpStatusCode.OK)]
        public abstract Task<IActionResult> Add([FromBody]UserDto userDto);


        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Response<UserDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<UserDto>), (int)HttpStatusCode.NotFound)]
        public abstract Task<IActionResult> Update([FromBody]UserDto userDto, [FromRoute] Guid id);

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<Guid>), (int)HttpStatusCode.NotFound)]
        public abstract Task<IActionResult> Delete([FromRoute] Guid id);
    }
}
