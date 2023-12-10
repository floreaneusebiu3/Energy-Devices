namespace MeasurementManagementService.Mappers.Dtos
{
    public class MeasurementDto
    {
        public double Consumption { get; set; }
        public long Timestamp { get; set; }
        public Guid UserDeviceId { get; set; }

        public override string ToString()
        {
            return $"TimeStamp: {Timestamp}  UserDeviceId: {UserDeviceId}   Measurement: {Consumption}";
        }
    }
}
