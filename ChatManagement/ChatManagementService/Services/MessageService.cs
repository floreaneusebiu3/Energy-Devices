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

public class MessageService: IMessageService
{
    private readonly BaseRepository<Message> _messageRepository;
    private readonly BaseRepository<User> _userRepository;
    private readonly BaseRepository<Group> _groupRepository;
    private readonly IHubContext<ClientHub> _webSocket;

    public MessageService(BaseRepository<Message> messageRepository, BaseRepository<User> userRepository, BaseRepository<Group> groupRepository, IHubContext<ClientHub> webSocket)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
        _groupRepository = groupRepository;
        _webSocket = webSocket;
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

        _messageRepository.Add(messageDto.ProjectToEntity(senderUserId));
        _messageRepository.SaveChanges();

        return new Response<GroupMessageDto>(messageDto);
    }

    public Response<List<MessageDto>> GetGroupMessages(Guid groupId)
    {
        if (!_groupRepository.Query(u => u.Id == groupId).Any())
        {
            return new Response<List<MessageDto>>(new GroupNotFoundException(Constants.API_GROUP_NOT_FOUND_EXCEPTION));
        }

        var messages = _messageRepository.Query(m => m.GroupId == groupId)
            .Include(m => m.SenderUser)
            .OrderByDescending(m => m.Timestamp)
            .ToList();
        foreach (Message message in messages)
        { 
            message.Seen = true;
        }
        _messageRepository.SaveChanges();

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
        foreach (Message message in messages)
        {
            message.Seen = true;
        }

        var messagesDto = messages.Select(m => m.ProjectToMessageDto())
                                  .ToList();
        return new Response<List<MessageDto>>(messagesDto);
    }
}
