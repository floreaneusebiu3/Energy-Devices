using DeviceManagementService.Mappers;
using DeviceManagementService.Mappers.Dtos;
using DevicesManagementDomain;
using DevicesManagementData.Repositories;
using DeviceManagementService.Common;
using DeviceManagementService.Exceptions;
using DeviceManagementService.Interfaces;
using Microsoft.EntityFrameworkCore;
using DeviceManagementService.Mappers.UserDeviceMappers.Dtos;
using DeviceManagementService.Mappers.UserDeviceDeviceMappers;
using DeviceManagementService.Services;
using DeviceManagementService.Services.Interfaces;

namespace DeviceManagementService
{
    public class DeviceService : IDeviceService
    {
        private readonly BaseRepository<Device> _deviceRepository;
        private readonly BaseRepository<UserDevice> _userDeviceRepository;
        private readonly IRabbitMqProducer _rabbitMqProducer;

        public DeviceService(BaseRepository<Device> deviceRepository, BaseRepository<UserDevice> userDeviceRepository, IRabbitMqProducer rabbitMqProducer)
        {
            _deviceRepository = deviceRepository;
            _userDeviceRepository = userDeviceRepository;
            _rabbitMqProducer = rabbitMqProducer;
        }

        public Response<DeviceDto> Add(DeviceDto deviceDto)
        {
            var device = deviceDto.ProjectToEntity();
            _deviceRepository.Add(device);
            _deviceRepository.SaveChanges();
            return new Response<DeviceDto>(device.ProjectToDto());
        }

        public Response<DeviceDto> Update(Guid id, DeviceDto deviceDto)
        {
            var device = _deviceRepository.Query(u => u.Id == id)
                .FirstOrDefault();
            if (device == null)
            {
                return new Response<DeviceDto>(new DeviceNotFoundException(Constants.DEVICE_NOT_FOUND));
            }

            deviceDto.Id = id;
            AssignUpdatedProperties(device, deviceDto);

            var userDevices = GetMonitoringUserDevices(id, deviceDto.MaximuHourlyEnergyConsumption);
            foreach (var userDevice in userDevices) 
            {
                _rabbitMqProducer.SendMessage(userDevice);
            }

            _deviceRepository.SaveChanges();
            return new Response<DeviceDto>(device.ProjectToDto());
        }

        private void AssignUpdatedProperties(Device device, DeviceDto deviceDto)
        {
            device.Name = deviceDto.Name;
            device.Description = deviceDto.Description;
            device.MaximuHourlyEnergyConsumption = deviceDto.MaximuHourlyEnergyConsumption;
        }

        private List<MonitoringUserDevice> GetMonitoringUserDevices(Guid deviceId, long maximumHourlyConsumption)
        {
            return _userDeviceRepository.Query(ud => ud.DeviceId == deviceId)
                .Select(ud => ud.ProjectToMonitoringUserDevice(maximumHourlyConsumption))
                .ToList();
        }

        public Response<Guid> Delete(Guid id)
        {
            var device = _deviceRepository.Query(d => d.Id == id)
                .FirstOrDefault();
            if (device == null)
            {
                return new Response<Guid>(new DeviceNotFoundException(Constants.DEVICE_NOT_FOUND));

            }
            _deviceRepository.Remove(device);
            _deviceRepository.SaveChanges();
            return new Response<Guid>(device.Id);
        }

        public Response<DeviceDto> GetById(Guid id)
        {
            var deviceDto = _deviceRepository.Query(u => u.Id == id)
                .ProjectToDto()
                .FirstOrDefault();
            return deviceDto != null ? new Response<DeviceDto>(deviceDto)
                                     : new Response<DeviceDto>(new DeviceNotFoundException(Constants.DEVICE_NOT_FOUND));
        }

        public Response<IEnumerable<DeviceDto>> FindAll()
        {
            var deviceDtoList = _deviceRepository.Query()
                .ProjectToDto()
                .ToList();
            return new Response<IEnumerable<DeviceDto>>(deviceDtoList);
        }

        public Response<IEnumerable<DeviceDto>> FindAllForUser(Guid id)
        {
            var deviceDtoList = _userDeviceRepository.Query(ud => ud.UserId.Equals(id))
                .Include(ud => ud.Device)
                .Select(ud => ud.Device)
                .ProjectToDto()
                .ToList();
            return new Response<IEnumerable<DeviceDto>>(deviceDtoList);
        }
    }
}
