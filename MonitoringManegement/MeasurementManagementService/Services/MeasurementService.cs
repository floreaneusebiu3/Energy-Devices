using MeasurementManagementService.Mappers;
using MeasurementManagementService.Mappers.Dtos;
using MeasurementManagementService.Services.Interfaces;
using MeasurementRepository;
using MonitoringManagementDomain;

namespace MeasurementManagementService.Services;

public class MeasurementService : IMeasurementService
{
    private readonly BaseRepository<Measurement> _measurementRepository;
    private readonly BaseRepository<UserDevice> _userDeviceRepository;

    public MeasurementService(BaseRepository<Measurement> measurementRepository, BaseRepository<UserDevice> userDeviceRepository)
    {
        _measurementRepository = measurementRepository;
        _userDeviceRepository = userDeviceRepository;
    }

    public MeasurementDto InsertMeasurement(MeasurementDto measurementDto)
    {
        var saveMeasurement = measurementDto.MapToEntity();
        _measurementRepository.Add(saveMeasurement);
        _measurementRepository.SaveChanges();
        return saveMeasurement.MapToDto();
    }

    public IEnumerable<HourlyMeasurement> GetForUserDeviceByDate(Guid userId, Guid deviceId, long timestamp)
    {
        ICollection<HourlyMeasurement> hourlyMeasurements = new List<HourlyMeasurement>();
        var userDeviceId = _userDeviceRepository.Query(ud => ud.UserId == userId && ud.DeviceId == deviceId)
        .Select(ud => ud.Id)
        .FirstOrDefault();
        var measurements = _measurementRepository.Query(m => m.UserDeviceId == userDeviceId)
            .OrderByDescending(ud => ud.TimeStamp)
            .ProjectQueryToDto()
            .ToList();
        var day = GetDayFromTimeStamp(timestamp);
        var dayMeasurements = measurements.Where(m => GetDayFromTimeStamp(m.Timestamp) == day).ToList();
        for (var hour = 0; hour <= 24; hour++)
        {
            var measurementsForHour = dayMeasurements.Where(m => GetHourFromTimeStamp(m.Timestamp) == hour).ToList();
            if (measurementsForHour.Count() > 5)
            {
                hourlyMeasurements.Add(
                    new HourlyMeasurement
                    {
                        Consumption = measurementsForHour.ElementAt(0).Consumption - measurementsForHour.ElementAt(5).Consumption,
                        Hour = hour,
                    });
            }
        }
        return hourlyMeasurements;
    }

    public WarningDto GetWarningForUserDevice(Guid userDeviceId)
    {
        List<Measurement> measurements = GetHourlyMeasurementsForUserDevice(userDeviceId);
        if (measurements.Count == 6)
        {
            var currentConsumption = measurements.ElementAt(0).Consumption - measurements.ElementAt(5).Consumption;
            var userDevice = _userDeviceRepository.Query(ud => ud.Id == userDeviceId)
                .FirstOrDefault();

            if (currentConsumption > userDevice.MaximuHourlyEnergyConsumption)
            {
                return new WarningDto(userDevice.UserId, userDevice.DeviceId, userDevice.MaximuHourlyEnergyConsumption, Math.Round(currentConsumption, 2));
            }
        }
        return null;
    }

    private List<Measurement> GetHourlyMeasurementsForUserDevice(Guid userDeviceId) =>
         _measurementRepository.Query(m => m.UserDeviceId == userDeviceId)
            .OrderByDescending(m => m.TimeStamp)
            .Take(6)
            .ToList();

    private int GetDayFromTimeStamp(long timestamp) =>
        DateTimeOffset.FromUnixTimeSeconds(timestamp).LocalDateTime.DayOfYear;

    private int GetHourFromTimeStamp(long timestamp) =>
        DateTimeOffset.FromUnixTimeSeconds(timestamp).LocalDateTime.Hour;
}
