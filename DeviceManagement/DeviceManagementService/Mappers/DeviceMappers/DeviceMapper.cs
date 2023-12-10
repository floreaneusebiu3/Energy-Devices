using DeviceManagementService.Mappers.Dtos;
using DevicesManagementDomain;

namespace DeviceManagementService.Mappers
{
    public static class DeviceMapper
    {
        public static IQueryable<DeviceDto> ProjectToDto(this IQueryable<Device> query)
        {
            return query.Select(device =>
            CreateDeviceDto(device.Id, device.Name,
            device.Description, device.MaximuHourlyEnergyConsumption)
            );
        }

        public static DeviceDto ProjectToDto(this Device device)
        {
            return CreateDeviceDto(device.Id, device.Name,
            device.Description, device.MaximuHourlyEnergyConsumption);
        }

        public static Device ProjectToEntity(this DeviceDto deviceDto)
        {
            return new Device
            {
                Name = deviceDto.Name,
                Description = deviceDto.Description,
                MaximuHourlyEnergyConsumption = deviceDto.MaximuHourlyEnergyConsumption
            };
        }

        private static DeviceDto CreateDeviceDto(Guid id, string name, string description, long maximumHourlyEnergyConsumption)
        {
            return new DeviceDto
            {
                Id = id,
                Name = name,
                Description = description,
                MaximuHourlyEnergyConsumption = maximumHourlyEnergyConsumption
            };
        }
    }
}
