using MeasurementManagementService.Mappers.Dtos;
using MeasurementManagementService.Services.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net.Sockets;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR;
using MeasurementManagementService.Gateway;
using MonitoringManagementDomain;
using Microsoft.Extensions.Configuration;

namespace MeasurementManagementService.Services;

public class RabitMqService : IRabitMqService
{
    private readonly IMeasurementService _measurementService;
    private readonly IUserDeviceService _userDeviceService;
    private readonly IHubContext<ClientHub> _webSocket;
    private readonly IConfiguration _config;
    public RabitMqService(IMeasurementService measurementService, IHubContext<ClientHub> webSocket, IUserDeviceService userDeviceService, IConfiguration config)
    {
        _measurementService = measurementService;
        _webSocket = webSocket;
        _userDeviceService = userDeviceService;
        _config = config;
    }

    public async void ListenToSensorQueue(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _config["Url:RabbitHostName"]
        };
        var connection = factory.CreateConnection();

        using var sensorChannel = connection.CreateModel();
        sensorChannel.QueueDeclare("sensorQueue", exclusive: false);
        var sensorConsumer = new EventingBasicConsumer(sensorChannel);

        sensorConsumer.Received += (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            MeasurementDto measurementDto = JsonSerializer.Deserialize<MeasurementDto>(body);
            measurementDto.Consumption = Math.Round(measurementDto.Consumption, 2);

            _measurementService.InsertMeasurement(measurementDto);

            WarningDto warning = _measurementService.GetWarningForUserDevice(measurementDto.UserDeviceId);
            if (warning is not null)
            {
                _webSocket.Clients.All.SendAsync(warning.UserId.ToString(), warning);
            }
        };

        using var userDeviceChannel = connection.CreateModel();
        userDeviceChannel.QueueDeclare("userDeviceQueue", exclusive: false);
        var userDeviceConsumer = new EventingBasicConsumer(sensorChannel);

        userDeviceConsumer.Received += (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            UserDevice userDevice = JsonSerializer.Deserialize<UserDevice>(body);

            UserDevice? existingUserDevice = _userDeviceService.Get(userDevice.Id);
            if (existingUserDevice is not null)
            {
                _userDeviceService.Update(userDevice);
            }
            else
            {
                _userDeviceService.Add(userDevice);
            }
        };

        while (!cancellationToken.IsCancellationRequested)
        {
            sensorChannel.BasicConsume(queue: "sensorQueue", autoAck: true, consumer: sensorConsumer);
            userDeviceChannel.BasicConsume(queue: "userDeviceQueue", autoAck: true, consumer: userDeviceConsumer);
        }
    }
}
