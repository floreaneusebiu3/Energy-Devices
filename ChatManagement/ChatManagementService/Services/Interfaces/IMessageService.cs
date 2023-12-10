using ChatManagementService.Common;
using ChatManagementService.Model;

namespace ChatManagementService.Services.Interfaces;

public interface IMessageService
{
    Response<UserMessageDto> SendMessageToUser(UserMessageDto messageDto);
    Response<GroupMessageDto> SendMessageToGroup(GroupMessageDto messageDto);
    Response<List<MessageDto>> GetGroupMessages(Guid groupId);
    Response<List<MessageDto>> GetUsersMessages(Guid currentUserId, Guid otherUserId);
}
