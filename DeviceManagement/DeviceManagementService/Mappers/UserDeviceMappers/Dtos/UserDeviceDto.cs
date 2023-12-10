
namespace DeviceManagementService.Mappers.UserDeviceMappers.Dtos
{
    public class UserDeviceDto
    {
        public Guid DeviceId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string DeviceName { get; set; }
        public string Description { get; set; }
        public string Address  { get; set; }
        public long MaximuHourlyEnergyConsumption { get; set; }
    }
}
