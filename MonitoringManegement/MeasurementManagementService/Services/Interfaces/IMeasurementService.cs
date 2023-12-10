using MeasurementManagementService.Mappers.Dtos;

namespace MeasurementManagementService.Services.Interfaces
{
    public interface IMeasurementService
    {
        public IEnumerable<HourlyMeasurement> GetForUserDeviceByDate(Guid userId, Guid deviceId, long timestamp);
        public MeasurementDto InsertMeasurement(MeasurementDto measurementDto);
        public WarningDto GetWarningForUserDevice(Guid userDeviceId);
    }
}
