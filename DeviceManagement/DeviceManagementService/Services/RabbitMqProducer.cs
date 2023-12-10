using DeviceManagementService.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace DeviceManagementService.Services;

public class RabbitMqProducer : IRabbitMqProducer
{
    private readonly IConfiguration _config;

    public RabbitMqProducer(IConfiguration config)
    {
        _config = config;
    }

    public void SendMessage<T>(T message)
    {
        var factory = new ConnectionFactory
        {
            HostName = _config.GetValue<string>("Url:rabbitHostName")
        };
        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare("userDeviceQueue", exclusive: false);
        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);
        channel.BasicPublish(exchange: "", routingKey: "userDeviceQueue", body: body);
    }
}

