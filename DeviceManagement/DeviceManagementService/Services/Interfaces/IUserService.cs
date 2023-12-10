using DeviceManagementService.Common;
using DeviceManagementService.Mappers.UserMappers.Dtos;

namespace DeviceManagementService.Interfaces
{
    public interface IUserService
    {
        public Response<string> Add(UserDto userDto);
        public Response<string> Update(UserDto userDto, Guid id);
        public Response<string> Delete(Guid id);
    }
}
