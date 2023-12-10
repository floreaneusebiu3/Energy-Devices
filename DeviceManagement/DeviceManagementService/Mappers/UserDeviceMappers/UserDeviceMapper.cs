using DeviceManagementService.Mappers.UserDeviceMappers.Dtos;
using DevicesManagementDomain;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagementService.Mappers.UserDeviceDeviceMappers
{
    public static class UserDeviceMapper
    {
        public static IQueryable<UserDeviceDto> ProjectToDto(this IQueryable<UserDevice> query)
        {
            return query.Include(ud => ud.Device)
                .Include(ud => ud.User)
                .Select(ud => new UserDeviceDto
                {
                    UserId = ud.UserId,
                    DeviceId = ud.DeviceId,
                    UserName = ud.User.Name,
                    DeviceName = ud.Device.Name,
                    Description = ud.Device.Description,
                    Address = ud.Address,
                    MaximuHourlyEnergyConsumption = ud.Device.MaximuHourlyEnergyConsumption
                });
        }

        public static MonitoringUserDevice ProjectToMonitoringUserDevice(this UserDevice userDevice, long maximulHourlyConsumption) =>
          new MonitoringUserDevice
          {
              Id = userDevice.Id,
              DeviceId = userDevice.DeviceId,
              UserId = userDevice.UserId,
              MaximuHourlyEnergyConsumption = maximulHourlyConsumption
          };

    }
}
