using UserManagement.Controllers.Dtos;
using UserManagementDomain;

namespace UserManagementService.Mappers
{
    public class UserMapper
    {   
        public User mapRegisterUserDtoToUser(RegisterUserDto registerUserDto)
        {
            return new User
            {
                Name = registerUserDto.Name,
                Email = registerUserDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password),
                Role = registerUserDto.Role.ToUpper()
            };
        }

        public User mapUserDtoToUser(UserDto userDto)
        {
            return new User
            {
                Id = userDto.Id,
                Name = userDto.Name,
                Email = userDto.Email,
                Role = userDto.Role.ToUpper()
            };
        }

        public UserDto mapUserToUserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}