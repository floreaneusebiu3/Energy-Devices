using ChatDomain;
using ChatManagementData.Repositories;
using ChatManagementService.Common;
using ChatManagementService.Exceptions;
using ChatManagementService.Mappers;
using ChatManagementService.Model;
using ChatManagementService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatManagementService.Services;

public class GroupService : IGroupService
{
    private readonly BaseRepository<Group> _groupRepository;
    private readonly BaseRepository<UserGroup> _userGroupRepository;
    private readonly BaseRepository<User> _userRepository;

    public GroupService(BaseRepository<Group> groupRepository, BaseRepository<UserGroup> userGroupRepository, BaseRepository<User> userRepository)
    {
        _groupRepository = groupRepository;
        _userGroupRepository = userGroupRepository;
        _userRepository = userRepository;
    }

    public Response<List<GroupDto>> GetGroupsForAdmin(Guid adminId)
    {
        var groups = _groupRepository.Query(g => g.AdminId == adminId)
                        .Include(g => g.UserGroups)
                        .ThenInclude(ug => ug.User)
                        .Select(g => g.MapToGroupDto())
                        .ToList();
        return new Response<List<GroupDto>>(groups);
    }

    public Response<List<GroupDto>> GetGroupsForUser(Guid userId)
    {
        var groups = _groupRepository.Query()
                        .Include(g => g.UserGroups)
                        .ThenInclude(ug => ug.User)
                        .Where(g => g.UserGroups.Any(ug => ug.UserId == userId))
                        .Select(g => g.MapToGroupDto())
                        .ToList();
        return new Response<List<GroupDto>>(groups);
    }

    public Response<CreateGroupDto> CreateGroup(Guid adminId, CreateGroupDto groupDto)
    {
        if (!_userRepository.Query(u => u.Id == adminId).Any())
        {
            return new Response<CreateGroupDto>(new UserNotFoundException(Constants.API_ADMIN_NOT_FOUND_EXCEPTION));
        }

        foreach (Guid userId in groupDto.UsersId)
        {
            if (_userRepository.Query(u => u.Id == userId && u.Role == "ADMIN").Any())
            {
                return new Response<CreateGroupDto>(new UserNotClientException(Constants.API_GROUP_MEMBERS_MUST_BE_CLIENTS_EXCEPTION));
            }
        }

        Group group = groupDto.MapToGroupEntity(adminId);
        _groupRepository.Add(group);
        _groupRepository.SaveChanges();

        foreach (var userId in groupDto.UsersId)
        {
            _userGroupRepository.Add(group.MapToUserGroup(userId));
            _userGroupRepository.SaveChanges();
        }

        return new Response<CreateGroupDto>(groupDto);
    }

    public Response<UserGroupDto> AddUserToGroup(Guid groupId, Guid userId)
    {
        var group = _groupRepository.Query(g => g.Id == groupId)
            .FirstOrDefault();

        if (group is null)
        {
            return new Response<UserGroupDto>(new GroupNotFoundException(Constants.API_GROUP_NOT_FOUND_EXCEPTION));
        }

        var user = _userRepository.Query(u => u.Id == userId).FirstOrDefault();
        if (user is null)
        {
            return new Response<UserGroupDto>(new UserNotFoundException(Constants.API_USER_NOT_FOUND_EXCEPTION));
        }

        if (user.Role == "ADMIN")
        {
            return new Response<UserGroupDto>(new UserNotClientException(Constants.API_GROUP_MEMBERS_MUST_BE_CLIENTS_EXCEPTION));
        }

        var existingUserInGroup = _userGroupRepository.Query(ug => ug.UserId == userId && ug.GroupId == groupId)
            .Any();
        if (existingUserInGroup)
        {
            return new Response<UserGroupDto>(new UserAlreadyInGroupException(Constants.API_USER_ALREADY_IN_GROUP_EXCEPTION));
        }

        var userGroup = group.MapToUserGroup(userId);
        _userGroupRepository.Add(userGroup);
        _userGroupRepository.SaveChanges();

        return new Response<UserGroupDto>(userGroup.MapToUserGroupDto());
    }
}
