namespace DeviceManagementService.Services.Interfaces;
public interface IRabbitMqProducer
{
    public void SendMessage<T>(T message);
}

