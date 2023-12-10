using DeviceManagementService.Common;
using DeviceManagementService.Exceptions;
using DeviceManagementService.Interfaces;
using DeviceManagementService.Mappers.UserDeviceDeviceMappers;
using DeviceManagementService.Mappers.UserDeviceDeviceMappers.Dtos;
using DeviceManagementService.Mappers.UserDeviceMappers.Dtos;
using DeviceManagementService.Services.Interfaces;
using DevicesManagementData.Repositories;
using DevicesManagementDomain;

namespace DeviceManagementService
{
    public class UserDeviceService: IUserDeviceService
    {
        private readonly BaseRepository<UserDevice> _userDeviceRepository;
        private readonly BaseRepository<Device> _deviceRepository;
        private readonly IRabbitMqProducer _rabbitMqProducer;

        public UserDeviceService(BaseRepository<UserDevice> userDeviceRepository, BaseRepository<Device> deviceRepository, IRabbitMqProducer rabbitMqProducer)
        {
            _userDeviceRepository = userDeviceRepository;
            _deviceRepository = deviceRepository;
            _rabbitMqProducer = rabbitMqProducer;
        }

        public Response<string> Add(UserDevicePostDto userDeviceDto)
        {
            var userDevice = new UserDevice
            {
                Id = Guid.NewGuid(),
                DeviceId = userDeviceDto.DeviceId,
                UserId = userDeviceDto.UserId,
                Address = userDeviceDto.Address
            };

            var maximumHourlyConsumption = _deviceRepository.Query(d => d.Id == userDeviceDto.DeviceId)
                .Select(d => d.MaximuHourlyEnergyConsumption)
                .FirstOrDefault();

            _rabbitMqProducer.SendMessage(userDevice.ProjectToMonitoringUserDevice(maximumHourlyConsumption));

            _userDeviceRepository.Add(userDevice);
            _userDeviceRepository.SaveChanges();
            return new Response<string>("added");
        }

        public Response<string> Delete(Guid userId, Guid deviceId)
        {
            var userDevice = _userDeviceRepository.Query(ud => ud.DeviceId.Equals(deviceId) && ud.UserId.Equals(userId))
                .FirstOrDefault();
            if (userDevice == null)
            {
                return new Response<string>(new UserDeviceNotFoundException(Constants.USER_DEVICE_NOT_FOUND));
            }
            _userDeviceRepository.Remove(userDevice);
            _userDeviceRepository.SaveChanges();
            return new Response<string>("deleted");
        }

        public Response<IEnumerable<UserDeviceDto>> ReadAll()
        {
            var userDeviceList = _userDeviceRepository.Query()
                .ProjectToDto()
                .ToList();
            return new Response<IEnumerable<UserDeviceDto>>(userDeviceList);    
        }
    }
}
