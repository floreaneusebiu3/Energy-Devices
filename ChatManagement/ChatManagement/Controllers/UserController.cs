using ChatManagementService.Common;
using ChatManagementService.Exceptions;
using ChatManagementService.Model;
using ChatManagementService.Services.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ChatManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase 
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserDto> _userValidator;

        public UserController(IUserService userService, IValidator<UserDto> userValidator)
        {
            _userService = userService;
            _userValidator = userValidator;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Response<UserDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<UserDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Response<UserDto>), (int)HttpStatusCode.BadRequest)]
        public IActionResult Insert([FromBody] UserDto userDto)
        {
            var validationResult = _userValidator.Validate(userDto);
            if (!validationResult.IsValid)
            {
                return FormatResponse(GetErrorValidationResponse<UserDto>(validationResult));
            }

            var result = _userService.Create(userDto);
            return FormatResponse(result);
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Response<UserDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<UserDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Response<UserDto>), (int)HttpStatusCode.BadRequest)]
        public IActionResult Update([FromBody] UserDto userDto)
        {
            var validationResult = _userValidator.Validate(userDto);
            if (!validationResult.IsValid)
            {
                return FormatResponse(GetErrorValidationResponse<UserDto>(validationResult));
            }

            var result = _userService.Update(userDto);
            return FormatResponse(result);
        }

        [HttpDelete("{UserId}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Response<UserDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<UserDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Response<UserDto>), (int)HttpStatusCode.BadRequest)]
        public IActionResult Delete( Guid UserId)
        {
            var result = _userService.Delete(UserId);
            return FormatResponse(result);
        }


        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Response<List<UserDto>>), (int)HttpStatusCode.OK)]
        public IActionResult GetAll()
        {
            var result = _userService.ReadAll();
            return FormatResponse(result);
        }

        private Response<T> GetErrorValidationResponse<T>(ValidationResult validationResult)
        {
            var errors = validationResult.Errors
                   .Select(e => new CustomException(e.ErrorMessage));
            return new Response<T>(errors);
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

    }
}
