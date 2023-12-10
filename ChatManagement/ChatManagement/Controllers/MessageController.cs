using ChatManagementService.Common;
using ChatManagementService.Exceptions;
using ChatManagementService.Model;
using ChatManagementService.Services;
using ChatManagementService.Services.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace ChatManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IValidator<UserMessageDto> _userMessageDtoValidator;
        private readonly IValidator<GroupMessageDto> _groupMessageDtoValidator;

        public MessageController(IMessageService messageService, IValidator<UserMessageDto> userMessageDtoValidator, IValidator<GroupMessageDto> groupMessageDtoValidator)
        {
            _messageService = messageService;
            _userMessageDtoValidator = userMessageDtoValidator;
            _groupMessageDtoValidator = groupMessageDtoValidator;
        }

        [HttpGet("Group/{GroupId}")]
        [Authorize(Roles = "ADMIN, CLIENT")]
        [ProducesResponseType(typeof(Response<List<MessageDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<List<MessageDto>>), (int)HttpStatusCode.NotFound)]
        public IActionResult GetGroupMessages(Guid GroupId)
        {
            var result = _messageService.GetGroupMessages(GroupId);
            return FormatResponse(result);
        }

        [HttpGet("User/{UserId}")]
        [Authorize(Roles = "ADMIN, CLIENT")]
        [ProducesResponseType(typeof(Response<List<MessageDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<List<MessageDto>>), (int)HttpStatusCode.NotFound)]
        public IActionResult GetUsersMessages(Guid UserId)
        {
            var currentUserId = GetIdFromToken();
            var result = _messageService.GetUsersMessages(currentUserId, UserId);
            return FormatResponse(result);
        }

        [HttpPost("GroupMessage")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Response<GroupMessageDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<GroupMessageDto>), (int)HttpStatusCode.NotFound)]
        public IActionResult SendMessageToGroup(GroupMessageDto messageDto)
        {
            var validationResult = _groupMessageDtoValidator.Validate(messageDto);
            if (!validationResult.IsValid)
            {
                return FormatResponse(GetErrorValidationResponse<GroupMessageDto>(validationResult));
            }
            var result = _messageService.SendMessageToGroup(messageDto);
            return FormatResponse(result);
        }

        [HttpPost("UserMessage")]
        [Authorize(Roles = "ADMIN, CLIENT")]
        [ProducesResponseType(typeof(Response<UserMessageDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response<UserMessageDto>), (int)HttpStatusCode.NotFound)]
        public IActionResult SendMessageToUser(UserMessageDto messageDto)
        {
            var validationResult = _userMessageDtoValidator.Validate(messageDto);
            if (!validationResult.IsValid)
            {
                return FormatResponse(GetErrorValidationResponse<UserMessageDto>(validationResult));
            }
            if (GetIdFromToken() != messageDto.SenderUserId) 
            {
                return Unauthorized();
            }
            var result = _messageService.SendMessageToUser(messageDto);
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
            if (response.Exceptions.Any(e => e is UserNotFoundException || e is GroupNotFoundException))
            {
                return NotFound(response);
            }
            return BadRequest(response);
        }

        private Guid GetIdFromToken()
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(_bearer_token) as JwtSecurityToken;
            var id = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
            return new Guid(id);
        }

    }
}
