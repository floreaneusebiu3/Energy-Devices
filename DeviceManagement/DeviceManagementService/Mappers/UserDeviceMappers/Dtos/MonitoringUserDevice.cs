namespace DeviceManagementService.Mappers.UserDeviceMappers.Dtos;

public class MonitoringUserDevice
{
    public Guid Id { get; set; }
    public long MaximuHourlyEnergyConsumption { get; set; }
    public Guid DeviceId { get; set; }
    public Guid UserId { get; set; }
}

