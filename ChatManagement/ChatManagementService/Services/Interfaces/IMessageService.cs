using ChatManagementService.Common;
using ChatManagementService.Model;

namespace ChatManagementService.Services.Interfaces;

public interface IMessageService
{
    void NotifyUserIsTyping(Guid currentUserId, Guid destinationUserId, int textLength);
    Response<UserMessageDto> SendMessageToUser(Guid senderUserId, UserMessageDto messageDto);
    Response<GroupMessageDto> SendMessageToGroup(Guid senderUserId, GroupMessageDto messageDto);
    Response<List<MessageDto>> GetGroupMessages(Guid groupId, string currentUserRole);
    Response<List<MessageDto>> GetUsersMessages(Guid currentUserId, Guid otherUserId);
}
