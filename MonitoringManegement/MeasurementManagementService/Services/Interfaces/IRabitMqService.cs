using MeasurementManagementService.Mappers.Dtos;

namespace MeasurementManagementService.Services.Interfaces
{
    public interface IRabitMqService
    {
        public void ListenToSensorQueue(CancellationToken cancellationToken);
    }
}
