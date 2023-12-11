using ChatDomain;
using ChatManagementService.Common;
using ChatManagementService.Model;

namespace ChatManagementService.Services.Interfaces;

public interface IGroupService
{
    Response<List<GroupDto>> GetGroupsForUser(Guid userId);
    Response<List<GroupDto>> GetGroupsForAdmin(Guid adminId);
    Response<CreateGroupDto> CreateGroup(Guid adminId, CreateGroupDto groupDto);
    Response<UserGroupDto> AddUserToGroup(Guid groupId, Guid userId);
}
