using ChatDomain;
using ChatManagementData.Repositories;
using ChatManagementService.Common;
using ChatManagementService.Exceptions;
using ChatManagementService.Gateway;
using ChatManagementService.Mappers;
using ChatManagementService.Model;
using ChatManagementService.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatManagementService.Services;

public class MessageService : IMessageService
{
    private readonly BaseRepository<Message> _messageRepository;
    private readonly BaseRepository<User> _userRepository;
    private readonly BaseRepository<Group> _groupRepository;
    private readonly BaseRepository<UserGroup> _userGroupRepository;
    private readonly IHubContext<ClientHub> _webSocket;

    public MessageService(BaseRepository<Message> messageRepository, BaseRepository<User> userRepository, BaseRepository<Group> groupRepository, IHubContext<ClientHub> webSocket, BaseRepository<UserGroup> userGroupRepository)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
        _groupRepository = groupRepository;
        _webSocket = webSocket;
        _userGroupRepository = userGroupRepository;
    }

    public Response<UserMessageDto> SendMessageToUser(Guid senderUserId, UserMessageDto messageDto)
    {
        if (!_userRepository.Query(u => u.Id == senderUserId).Any() ||
            !_userRepository.Query(u => u.Id == messageDto.DestionationUserId).Any())
        {
            return new Response<UserMessageDto>(new UserNotFoundException(Constants.API_USER_NOT_FOUND_EXCEPTION));
        }

        _messageRepository.Add(messageDto.ProjectToEntity(senderUserId));
        _messageRepository.SaveChanges();

        _webSocket.Clients.All.SendAsync(messageDto.DestionationUserId.ToString(), "newMessage");

        return new Response<UserMessageDto>(messageDto);
    }

    public Response<GroupMessageDto> SendMessageToGroup(Guid senderUserId, GroupMessageDto messageDto)
    {
        if (!_userRepository.Query(u => u.Id == senderUserId).Any())
        {
            return new Response<GroupMessageDto>(new UserNotFoundException(Constants.API_USER_NOT_FOUND_EXCEPTION));
        }

        if (!_groupRepository.Query(u => u.Id == messageDto.GroupId).Any())
        {
            return new Response<GroupMessageDto>(new GroupNotFoundException(Constants.API_GROUP_NOT_FOUND_EXCEPTION));
        }

        var groupMembersId = _userGroupRepository.Query(ug => ug.GroupId == messageDto.GroupId)
            .Select(ug => ug.UserId)
            .ToList();
        foreach (var memberId in groupMembersId)
        {
            _webSocket.Clients.All.SendAsync(memberId.ToString(), "newMessage");
        }

        _messageRepository.Add(messageDto.ProjectToEntity(senderUserId));
        _messageRepository.SaveChanges();

        return new Response<GroupMessageDto>(messageDto);
    }

    public Response<List<MessageDto>> GetGroupMessages(Guid groupId, string currentUserRole)
    {
        if (!_groupRepository.Query(u => u.Id == groupId).Any())
        {
            return new Response<List<MessageDto>>(new GroupNotFoundException(Constants.API_GROUP_NOT_FOUND_EXCEPTION));
        }

        var messages = _messageRepository.Query(m => m.GroupId == groupId)
            .Include(m => m.SenderUser)
            .OrderByDescending(m => m.Timestamp)
            .ToList();

        if (currentUserRole == "CLIENT")
        {
            CheckGroupUnseenMessages(messages, groupId);
        }

        var messagesDto = messages.Select(m => m.ProjectToMessageDto())
                                  .ToList();
        return new Response<List<MessageDto>>(messagesDto);
    }

    public Response<List<MessageDto>> GetUsersMessages(Guid currentUserId, Guid otherUserId)
    {
        if (!_userRepository.Query(u => u.Id == currentUserId).Any() ||
        !_userRepository.Query(u => u.Id == otherUserId).Any())
        {
            return new Response<List<MessageDto>>(new UserNotFoundException(Constants.API_USER_NOT_FOUND_EXCEPTION));
        }

        var messages = _messageRepository.Query(m => (m.SenderUserId == currentUserId && m.DestionationUserId == otherUserId)
        || (m.SenderUserId == otherUserId && m.DestionationUserId == currentUserId))
            .Include(m => m.SenderUser)
            .OrderByDescending(m => m.Timestamp)
            .ToList();

        CheckUnseenMessages(messages, otherUserId);

        var messagesDto = messages.Select(m => m.ProjectToMessageDto())
                                  .ToList();
        return new Response<List<MessageDto>>(messagesDto);
    }

    private void CheckUnseenMessages(List<Message> messages, Guid destinationUserId)
    {
        bool wasUnseenMessage = false;
        foreach (Message message in messages)
        {
            if (message.Seen == false && message.SenderUserId == destinationUserId)
            {
                message.Seen = true;
                wasUnseenMessage = true;
            }
        }

        if (wasUnseenMessage)
        {
            _messageRepository.SaveChanges();
            _webSocket.Clients.All.SendAsync(destinationUserId.ToString(), "newSeen");
        }
    }

    private void CheckGroupUnseenMessages(List<Message> messages, Guid groupId)
    {
        bool wasUnseenMessage = false;
        foreach (Message message in messages)
        {
            if (message.Seen == false && message.GroupId == groupId)
            {
                message.Seen = true;
                wasUnseenMessage = true;
            }
        }

        if (wasUnseenMessage)
        {
            var groupAdminId = _groupRepository.Query(g => g.Id == groupId)
                .Select(g => g.AdminId)
                .FirstOrDefault();
            _messageRepository.SaveChanges();
            _webSocket.Clients.All.SendAsync(groupAdminId.ToString(), "newSeen");
        }
    }

    public void NotifyUserIsTyping(Guid currentUserId, Guid destinationUserId, int textLength)
    {
        var adminName = _groupRepository.Query(g => g.Id == destinationUserId)
            .Include(g => g.Admin)
            .Select(g => g.Admin.Name)
            .FirstOrDefault();
        if (adminName != null)
        {
            var groupMembersId = _userGroupRepository.Query(ug => ug.GroupId == destinationUserId)
                .Select(ug => ug.UserId)
                .ToList();
            if (textLength > 0)
            {
                foreach (var memberId in groupMembersId)
                    _webSocket.Clients.All.SendAsync(memberId.ToString(), $"{adminName} {destinationUserId} is typing in group...");
            }
            else
            {
                foreach (var memberId in groupMembersId)
                    _webSocket.Clients.All.SendAsync(memberId.ToString(), $"{adminName} stopped");
            }
            return;
        }

        var userId = _userRepository.Query(u => u.Id == currentUserId)
             .Select(u => u.Id)
             .FirstOrDefault();
        if (textLength > 0)
        {
            _webSocket.Clients.All.SendAsync(destinationUserId.ToString(), $"{userId}  is typing...");
        }
        else
        {
            _webSocket.Clients.All.SendAsync(destinationUserId.ToString(), $"{userId} stopped");
        }
    }
}
