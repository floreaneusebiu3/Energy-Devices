using ChatManagementService.Common;
using ChatManagementService.Model;

namespace ChatManagementService.Services.Interfaces;

public interface IUserService
{
    Response<UserDto> Create(UserDto user);
    Response<UserDto> Update(UserDto user);
    Response<UserDto> Delete(Guid UserId);
    Response<List<UserDto>> ReadAll();
}
