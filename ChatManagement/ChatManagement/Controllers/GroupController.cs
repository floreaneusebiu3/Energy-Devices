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
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IValidator<CreateGroupDto> _groupValidator;

        public GroupController(IGroupService groupService, IValidator<CreateGroupDto> groupValidator)
        {
            _groupService = groupService;
            _groupValidator = groupValidator;
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("Admin/{AdminId}")]
        [ProducesResponseType(typeof(Response<List<GroupDto>>), (int)HttpStatusCode.OK)]
        public IActionResult GetAllForAdmin( Guid AdminId)
        {
            var result = _groupService.GetGroupsForAdmin(AdminId);
            return FormatResponse(result);
        }

        [HttpGet("User/{UserId}")]
        [Authorize(Roles = "ADMIN, CLIENT")]
        [ProducesResponseType(typeof(Response<List<GroupDto>>), (int)HttpStatusCode.OK)]
        public IActionResult GetAllForUser( Guid UserId)
        {
            var result = _groupService.GetGroupsForUser(UserId);
            return FormatResponse(result);
        }


        [HttpPost("CreateGroup")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Response<CreateGroupDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<CreateGroupDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Response<CreateGroupDto>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CreateGroup([FromBody] CreateGroupDto createGroupDto)
        {
            var validationResult = _groupValidator.Validate(createGroupDto);
            if (!validationResult.IsValid)
            {
                return FormatResponse(GetErrorValidationResponse<CreateGroupDto>(validationResult));
            }

            var result = _groupService.CreateGroup(createGroupDto);
            return FormatResponse(result);
        }

        [HttpPost("AddUserToGroup/{GroupId}/{UserId}")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Response<UserGroupDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<UserGroupDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Response<UserGroupDto>), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddUserToGroup(Guid GroupId, Guid UserId)
        { 
            var result = _groupService.AddUserToGroup(GroupId, UserId);
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
            if (response.Exceptions.Any(e => e is UserNotFoundException || e is UserNotClientException || e is GroupNotFoundException))
            {
                return NotFound(response);
            }
            return BadRequest(response);
        }
    }
}