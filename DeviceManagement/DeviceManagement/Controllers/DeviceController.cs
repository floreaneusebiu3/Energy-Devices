using DeviceManagementService;
using DeviceManagementService.Common;
using DeviceManagementService.Exceptions;
using DeviceManagementService.Interfaces;
using DeviceManagementService.Mappers.Dtos;
using DevicesManagementConsole.Controllers.Abstractions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagementConsole.Controllers
{
    public class DeviceController : AbstractDeviceController
    {
        private readonly IDeviceService _deviceService;
        private readonly IValidator<DeviceDto> _deviceDtoValidator;

        public DeviceController(IDeviceService deviceService, IValidator<DeviceDto> deviceDtoValidator)
        {
            _deviceService = deviceService;
            _deviceDtoValidator = deviceDtoValidator;
        }

        public override async Task<IActionResult> Add(DeviceDto deviceDto)
        {
            var validationResult = _deviceDtoValidator.Validate(deviceDto);
            if (!validationResult.IsValid)
            {
                return FormatResponse(getErrorValidationResponse<DeviceDto>(validationResult));
            }
            var result = await Task.FromResult(_deviceService.Add(deviceDto));
            return FormatResponse(result);
        }

        public override async Task<IActionResult> Update(DeviceDto deviceDto, Guid id)
        {
            var validationResult = _deviceDtoValidator.Validate(deviceDto);
            if (!validationResult.IsValid)
            {
                return FormatResponse(getErrorValidationResponse<DeviceDto>(validationResult));
            }
            var result = await Task.FromResult(_deviceService.Update(id, deviceDto));
            return FormatResponse(result);
        }

        public override async Task<IActionResult> Delete(Guid id)
        {
            var result = await Task.FromResult(_deviceService.Delete(id));
            return FormatResponse(result);
        }

        public override async Task<IActionResult> ReadAll()
        {
            var result = await Task.FromResult(_deviceService.FindAll());
            return FormatResponse(result);
        }

        public override async Task<IActionResult> ReadById(Guid id)
        {
            var result = await Task.FromResult(_deviceService.GetById(id));
            return FormatResponse(result);
        }

        public override async Task<IActionResult> ReadAllForUser(Guid id)
        {
            var result = await Task.FromResult(_deviceService.FindAllForUser(id));
            return FormatResponse(result);
        }

        private IActionResult FormatResponse<T>(Response<T> response)
        {
            if (!response.isException)
            {
                return Ok(response);
            }
            if (response.Exceptions.Any(e => e is DeviceNotFoundException))
            {
                return NotFound(response);
            }
            return BadRequest(response);
        }

        private Response<T> getErrorValidationResponse<T>(ValidationResult validationResult)
        {
            var errors = validationResult.Errors
                   .Select(e => new CustomValidationException(e.ErrorMessage));
            return new Response<T>(errors);
        }
    }
}
