using DeviceManagementService.Common;
using DeviceManagementService.Mappers.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevicesManagementConsole.Controllers.Abstractions
{
    [ApiController]
    [Route("devices")]
    public abstract class AbstractDeviceController : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Response<DeviceDto>), (int)HttpStatusCode.OK)]
        public abstract Task<IActionResult> Add([FromBody] DeviceDto deviceDto);

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Response<DeviceDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<DeviceDto>), (int)HttpStatusCode.NotFound)]
        public abstract Task<IActionResult> Update([FromBody] DeviceDto deviceDto, [FromRoute] Guid id);

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Response<DeviceDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<DeviceDto>), (int)HttpStatusCode.NotFound)]
        public abstract Task<IActionResult> Delete([FromRoute] Guid id);

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Response<DeviceDto[]>), (int)HttpStatusCode.OK)]
        public abstract Task<IActionResult> ReadAll();

        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Response<DeviceDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<DeviceDto>), (int)HttpStatusCode.NotFound)]
        public abstract Task<IActionResult> ReadById([FromRoute] Guid id);

        [HttpGet("all/{id}")]
        [Authorize]
        [ProducesResponseType(typeof(Response<DeviceDto[]>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<DeviceDto[]>), (int)HttpStatusCode.NotFound)]
        public abstract Task<IActionResult> ReadAllForUser([FromRoute] Guid id);
    }
}
