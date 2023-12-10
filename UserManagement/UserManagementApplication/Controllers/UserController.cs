using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Controllers.Dtos;
using UserManagement.Controllers.Interfaces;
using UserManagementService.Common;
using UserManagementService.Exceptions;
using UserManagementService.Interfaces;

namespace UserManagement.Controllers
{
    public class UserController : AbstractUserController
    {
        private readonly IUserService _userService;
        private readonly IValidator<RegisterUserDto> _registerUserDtoValidator;
        private readonly IValidator<UserDto> _userDtoValidator;

        public UserController(IUserService userService, IValidator<RegisterUserDto> registerUserDtoValidator, IValidator<UserDto> userDtoValidator)
        {
            _userService = userService;
            _registerUserDtoValidator = registerUserDtoValidator;
            _userDtoValidator = userDtoValidator;
        }

        public override async Task<IActionResult> FindAll()
        {
            var readAllResult = await _userService.FindAll();
            return FormatResponse(readAllResult);
        }

        public override async Task<IActionResult> Add(RegisterUserDto userDto)
        {
            var validationResult = _registerUserDtoValidator.Validate(userDto);
            if (!validationResult.IsValid)
            {
                return FormatResponse(getErrorValidationResponse<UserDto>(validationResult)); 
            }
            var addResult = await _userService.Add(userDto);
            return FormatResponse(addResult);
        }

        public override async Task<IActionResult> Update(UserDto userDto, Guid id)
        {
            var validationResult = _userDtoValidator.Validate(userDto);
            if (!validationResult.IsValid)
            {
                return FormatResponse(getErrorValidationResponse<UserDto>(validationResult));
            }
            var updateResponse = await _userService.Update(userDto, id);
            return FormatResponse(updateResponse);
        }

        public override async Task<IActionResult> Delete(Guid id)
        {
            var deleteResponse = await _userService.Delete(id);
            return FormatResponse(deleteResponse);
        }

        private IActionResult FormatResponse<T>(Response<T> response)
        {
            if (!response.isException)
            {
                return Ok(response);
            }
            if (response.Exceptions.Any(e => e is UserAlreadyExistingException))
            {
                return Conflict(response);
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
