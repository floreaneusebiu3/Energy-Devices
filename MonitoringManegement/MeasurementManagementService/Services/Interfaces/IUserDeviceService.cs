using MonitoringManagementDomain;

namespace MeasurementManagementService.Services.Interfaces;

public interface IUserDeviceService
{
    void Add(UserDevice userDevice);
    UserDevice? Get(Guid id);
    void Update(UserDevice userDevice);
}
