namespace MeasurementManagementService.Mappers.Dtos;

public class WarningDto
{
    public Guid UserId { get; set; }
    public Guid DeviceId { get; set; }
    public double MaximumConsumption { get; set; }
    public double CurrentConsumption { get; set; }

    public WarningDto(Guid userId, Guid deviceId, double maximumConsumption, double currentConsumption)
    {
        UserId = userId;
        DeviceId = deviceId;
        MaximumConsumption = maximumConsumption;
        CurrentConsumption = currentConsumption;
    }
}
