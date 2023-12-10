using UserManagement.Controllers.Dtos;
using UserManagementDomain;
using UserManagementService.Common;

namespace UserManagementService
{
    public interface IAuthenticationService
    {
        Response<string> authenticateUser(LoginDto loginDto);
        Task<Response<UserDto>> RegisterUser(RegisterUserDto registerUserDto);
    }
}