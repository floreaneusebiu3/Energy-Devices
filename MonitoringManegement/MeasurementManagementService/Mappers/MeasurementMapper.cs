using MeasurementManagementService.Mappers.Dtos;
using MonitoringManagementDomain;

namespace MeasurementManagementService.Mappers
{
    public static class MeasurementMapper
    {
        public static Measurement MapToEntity(this MeasurementDto measurementDto) =>
            new Measurement
            {
                Id = Guid.NewGuid(),
                TimeStamp = measurementDto.Timestamp,
                Consumption = measurementDto.Consumption,
                UserDeviceId = measurementDto.UserDeviceId,
            };

        public static MeasurementDto MapToDto(this Measurement measurement) =>
            new MeasurementDto
            {
                Timestamp = measurement.TimeStamp,
                Consumption = measurement.Consumption,
                UserDeviceId = measurement.UserDeviceId,
            };

        public static ICollection<MeasurementDto> ProjectQueryToDto(this IQueryable<Measurement> measurementQuery) =>
                measurementQuery.Select(ud => new MeasurementDto
                {
                    UserDeviceId = ud.UserDeviceId,
                    Timestamp = ud.TimeStamp, 
                    Consumption = ud.Consumption,
                }).ToList();
            
    }
}
