using ChatDomain;
using ChatManagementService.Model;

namespace ChatManagementService.Mappers;

public static class UserMapper
{
    public static User MapToEntity(this UserDto userDto) => new()
    {
        Id = userDto.Id,
        Name = userDto.Name,
        Role = userDto.Role.ToUpper(),
    };

    public static UserDto MapToDto(this User user) => new()
    {
        Id = user.Id,
        Name = user.Name,
        Role = user.Role,
    };
}
