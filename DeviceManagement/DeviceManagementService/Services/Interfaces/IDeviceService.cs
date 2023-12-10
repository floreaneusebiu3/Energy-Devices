using DeviceManagementService.Common;
using DeviceManagementService.Mappers.Dtos;

namespace DeviceManagementService.Interfaces
{
    public interface IDeviceService
    {
        public Response<DeviceDto> Add(DeviceDto deviceDto);
        public Response<DeviceDto> Update(Guid id, DeviceDto deviceDto);
        public Response<Guid> Delete(Guid id);
        public Response<DeviceDto> GetById(Guid id);
        public Response<IEnumerable<DeviceDto>> FindAll();
        public Response<IEnumerable<DeviceDto>> FindAllForUser(Guid id);

    }
}
