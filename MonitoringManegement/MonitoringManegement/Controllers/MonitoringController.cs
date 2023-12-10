using MeasurementManagementService.Mappers.Dtos;
using MeasurementManagementService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MonitoringManegement.Common;
using System.Net;

namespace MonitoringManegement.Controllers
{
    [ApiController]
    [Route("monitoring")]
    public class MonitoringController: ControllerBase
    {
        private readonly IMeasurementService _measurementService;

        public MonitoringController(IMeasurementService measurementService)
        {
            _measurementService = measurementService;
        }

        [HttpGet("{userId}/{deviceId}/{timestamp}")]
        [ProducesResponseType(typeof(Response<IEnumerable<HourlyMeasurement>>), (int)HttpStatusCode.OK)]
        public IActionResult FindAllForUserDeviceByDate([FromRoute] Guid userId, [FromRoute] Guid deviceId, [FromRoute] long timestamp)
        { 
            return Ok(new Response<IEnumerable<HourlyMeasurement>>(_measurementService.GetForUserDeviceByDate(userId, deviceId, timestamp)));
        }
    }
}
