using DeviceManagementService.Common;
using DeviceManagementService.Mappers.UserDeviceDeviceMappers.Dtos;
using DeviceManagementService.Mappers.UserDeviceMappers.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevicesManagementConsole.Controllers.Abstractions
{
    [ApiController]
    [Route("devices-userDevices")]
    public abstract class AbstractUserDeviceController : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Response<UserDeviceDto>), (int)HttpStatusCode.OK)]
        public abstract Task<IActionResult> Add([FromBody] UserDevicePostDto userDeviceDto);

        [HttpDelete("{userId}/{deviceId}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Response<UserDeviceDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<UserDeviceDto>), (int)HttpStatusCode.NotFound)]
        public abstract Task<IActionResult> Delete([FromRoute] Guid userId, [FromRoute] Guid deviceId);

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Response<IEnumerable<UserDeviceDto>>), (int)HttpStatusCode.OK)]
        public abstract Task<IActionResult> ReadAll();
    }
}
