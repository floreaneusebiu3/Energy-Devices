using DeviceManagement.Controllers.Abstractions;
using DeviceManagementService.Common;
using DeviceManagementService.Exceptions;
using DeviceManagementService.Interfaces;
using DeviceManagementService.Mappers.UserMappers.Dtos;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagement.Controllers
{
    public class UserController : AbstractUserController
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserDto> _userDtoValidator;

        public UserController(IUserService userService, IValidator<UserDto> userValidator)
        {
            _userService = userService;
            _userDtoValidator = userValidator;
        }

        public async override Task<IActionResult> Add(UserDto userDto)
        {
            var validationResult = _userDtoValidator.Validate(userDto);
            if (!validationResult.IsValid)
            {
                return FormatResponse(getErrorValidationResponse<UserDto>(validationResult));
            }
            var result = await Task.FromResult(_userService.Add(userDto));
            return FormatResponse(result);
        }

        public async override Task<IActionResult> Update(UserDto userDto, Guid id)
        {
            var validationResult = _userDtoValidator.Validate(userDto);
            if (!validationResult.IsValid)
            {
                return FormatResponse(getErrorValidationResponse<UserDto>(validationResult));
            }
            var result =  await Task.FromResult(_userService.Update(userDto, id));
            return FormatResponse(result);
        }

        public async override Task<IActionResult> Delete(Guid id)
        {
            var result = await Task.FromResult(_userService.Delete(id));
            return FormatResponse(result);
        }

        private IActionResult FormatResponse<T>(Response<T> response)
        {
            if (!response.isException)
            {
                return Ok(response);
            }
            if (response.Exceptions.Any(e => e is UserNotFoundException))
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
