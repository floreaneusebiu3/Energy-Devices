using DeviceManagementService.Common;
using DeviceManagementService.Mappers.UserDeviceDeviceMappers.Dtos;
using DeviceManagementService.Mappers.UserDeviceMappers.Dtos;

namespace DeviceManagementService.Interfaces
{
    public interface IUserDeviceService
    {
        public Response<string> Add(UserDevicePostDto userDto);
        public Response<string> Delete(Guid userId, Guid deviceId);
        public Response<IEnumerable<UserDeviceDto>> ReadAll();
    }
}
