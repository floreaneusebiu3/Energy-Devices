using DeviceManagementService.Mappers.UserMappers.Dtos;
using DevicesManagementDomain;

namespace DeviceManagementService.Mappers.UserMappers
{
    public static class UserMapper
    {
        public static UserDto ProjectToDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public static User ProjectToEntity(this UserDto user)
        {
            return new User
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
