using ChatDomain;
using ChatManagementService.Model;
using System;

namespace ChatManagementService.Mappers;

public static class MessageMapper
{
    public static Message ProjectToEntity(this UserMessageDto userMessageDto, Guid senderUserId) => new()
    { 
        MessageText = userMessageDto.MessageText,
        SenderUserId = senderUserId,
        DestionationUserId = userMessageDto.DestionationUserId,
        Seen = false,
        Timestamp = (int)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
    };
    
    public static Message ProjectToEntity(this GroupMessageDto userMessageDto, Guid senderUserId) => new()
    { 
        MessageText = userMessageDto.MessageText,
        SenderUserId = senderUserId,
        GroupId = userMessageDto.GroupId,
        Seen = false,
        Timestamp = (int)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
    };

    public static MessageDto ProjectToMessageDto(this Message message) => new()
    {
        MessageText = message.MessageText,
        SenderId = message.SenderUserId,
        SenderName = message.SenderUser.Name,
        DestionationUserId = message.DestionationUserId != null ? message.DestionationUserId.Value : message.GroupId.Value,
    };
}
