using UserManagement.Controllers.Dtos;
using UserManagementService.Common;

namespace UserManagementService.Interfaces
{
    public interface IUserService
    {
        Task<Response<IEnumerable<UserDto>>> FindAll();
        Task<Response<UserDto>> Update(UserDto userDto, Guid userId);
        Task<Response<Guid>> Delete(Guid id);
        Task<Response<UserDto>> Add(RegisterUserDto userDto);
    }
}
