using MeasurementManagementService.Services.Interfaces;
using MeasurementRepository;
using MonitoringManagementDomain;

namespace MeasurementManagementService.Services;

public class UserDeviceService : IUserDeviceService
{
    private readonly BaseRepository<UserDevice> _userDeviceRepository;

    public UserDeviceService(BaseRepository<UserDevice> userDeviceRepository)
    {
        _userDeviceRepository = userDeviceRepository;
    }

    public void Add(UserDevice userDevice)
    {
        _userDeviceRepository.Add(userDevice);
        _userDeviceRepository.SaveChanges();
    }

    public UserDevice? Get(Guid id)
    {
        return _userDeviceRepository.Query(ud => ud.Id == id)
            .FirstOrDefault();
    }

    public void Update(UserDevice userDevice)
    {
        var existingUserDevice = _userDeviceRepository.Query(ud => ud.Id == userDevice.Id)
            .FirstOrDefault();
        if (existingUserDevice is not null)
        {
            existingUserDevice.DeviceId = userDevice.DeviceId;
            existingUserDevice.UserId = userDevice.UserId;
            existingUserDevice.MaximuHourlyEnergyConsumption = userDevice.MaximuHourlyEnergyConsumption;
            _userDeviceRepository.SaveChanges();
        }
    }
}

