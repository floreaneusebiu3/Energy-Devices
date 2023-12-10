using Microsoft.AspNetCore.Mvc;
using UserManagement.Controllers.Abstractions;
using UserManagement.Controllers.Dtos;
using UserManagementService;
using UserManagementService.Common;
using FluentValidation.Results;
using UserManagementService.Exceptions;
using FluentValidation;

namespace UserManagement.Controllers
{

    public class AuthenticationController : AbstractAuthenticationController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IValidator<RegisterUserDto> _registerUserDtoValidator;
        private readonly IValidator<LoginDto> _loginDtoValidator;


        public AuthenticationController(IAuthenticationService authenticationService, IValidator<RegisterUserDto> registerUserDtoValidator, IValidator<LoginDto> loginDtoValidator)
        {
            _authenticationService = authenticationService;
            _registerUserDtoValidator = registerUserDtoValidator;
            _loginDtoValidator = loginDtoValidator;
        }

        public override async Task<IActionResult> Login(LoginDto loginDto)
        {
            var validationResult = _loginDtoValidator.Validate(loginDto);
            if (!validationResult.IsValid) 
            {
                return FormatResponse(getErrorValidationResponse<string>(validationResult));
            }
            var loginResult = await Task.FromResult(_authenticationService.authenticateUser(loginDto));
            return FormatResponse(loginResult);
        }

        public override async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            var validationResult = _registerUserDtoValidator.Validate(registerUserDto);
            if (!validationResult.IsValid)
            {
                return FormatResponse(getErrorValidationResponse<UserDto>(validationResult));
            }
            var registerResult = await _authenticationService.RegisterUser(registerUserDto);
            return FormatResponse(registerResult);
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