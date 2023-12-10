using DeviceManagementService.Common;
using DeviceManagementService.Exceptions;
using DeviceManagementService.Interfaces;
using DeviceManagementService.Mappers.UserDeviceDeviceMappers.Dtos;
using DeviceManagementService.Mappers.UserDeviceMappers.Dtos;
using DevicesManagementConsole.Controllers.Abstractions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagementConsole.Controllers
{
    public class UserDeviceController : AbstractUserDeviceController
    {
        private readonly IUserDeviceService _userDeviceService;
        private readonly IValidator<UserDevicePostDto> _userDevicePostDtoValidator;

        public UserDeviceController(IUserDeviceService userDeviceService, IValidator<UserDevicePostDto> userDevicePostDtoValidator)
        {
            _userDeviceService = userDeviceService;
            _userDevicePostDtoValidator = userDevicePostDtoValidator;
        }

        public override async Task<IActionResult> Add(UserDevicePostDto userDto)
        {
            var validationResult = _userDevicePostDtoValidator.Validate(userDto);
            if (!validationResult.IsValid)
            {
                return FormatResponse(getErrorValidationResponse<UserDeviceDto>(validationResult));
            }
            var result = await Task.FromResult(_userDeviceService.Add(userDto));
            return FormatResponse(result);
        }

        public override async Task<IActionResult> Delete(Guid userId, Guid deviceId)
        {
            var result = await Task.FromResult(_userDeviceService.Delete(userId, deviceId));
            return FormatResponse(result);
        }

        public override async Task<IActionResult> ReadAll()
        {
            var result = await Task.FromResult(_userDeviceService.ReadAll());
            return FormatResponse(result);
        }

        private IActionResult FormatResponse<T>(Response<T> response)
        {
            if (!response.isException)
            {
                return Ok(response);
            }
            if (response.Exceptions.Any(e => e is UserDeviceNotFoundException))
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
