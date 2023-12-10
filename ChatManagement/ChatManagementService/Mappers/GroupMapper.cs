using ChatDomain;
using ChatManagementService.Model;
using Microsoft.EntityFrameworkCore;

namespace ChatManagementService.Mappers;

public static class GroupMapper
{
    public static Group MapToGroupEntity(this CreateGroupDto groupDto) => new()
    {
        AdminId = groupDto.AdminId,
        Name = groupDto.Name,
    };

    public static GroupDto MapToGroupDto(this Group group) => new()
    {
        AdminId = group.AdminId,
        Name = group.Name,
        Id = group.Id,
        Members = group.UserGroups.Select(ug => ug.User.Name).ToList(),
    };

    public static UserGroup MapToUserGroup(this Group group, Guid userId) => new()
    {
        UserId = userId,
        GroupId = group.Id
    };

    public static UserGroupDto MapToUserGroupDto(this UserGroup userGroup) => new()
    {
        UserId = userGroup.UserId,
        GroupId = userGroup.Id,
    };
}
