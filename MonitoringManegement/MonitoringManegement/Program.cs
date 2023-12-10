using MeasurementManagementService.Gateway;
using MeasurementManagementService.Services;
using MeasurementManagementService.Services.Interfaces;
using MeasurementRepository;
using MonitoringManagementDomain;

var builder = WebApplication.CreateBuilder(args);

IConfiguration _config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var NSwagSpecificOrigins = "_nswaggPolicy";
var AngularSpecificOrigins = "_ngularAppPolicy";
var RabbitMqSpecificOrigins = "_rabbitMqPolicy";

builder.Services.AddCors(p =>
{
    p.AddPolicy(NSwagSpecificOrigins, builder =>
        builder.WithOrigins("*")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin());
    p.AddPolicy(AngularSpecificOrigins, builder =>
        builder.WithOrigins("*")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin()
        );
    p.AddPolicy(RabbitMqSpecificOrigins, builder =>
        builder.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins("http://localhost:4200"));
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddSingleton<BaseRepository<Measurement>>();
builder.Services.AddSingleton<BaseRepository<UserDevice>>();
builder.Services.AddSingleton<IMeasurementService, MeasurementService>();
builder.Services.AddSingleton<IUserDeviceService, UserDeviceService>();
builder.Services.AddSingleton<IRabitMqService, RabitMqService>();

var app = builder.Build();

app.UseCors(RabbitMqSpecificOrigins);
app.UseCors(NSwagSpecificOrigins);
app.UseCors(AngularSpecificOrigins);

app.UseWebSockets(new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromSeconds(120),
});

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ClientHub>(_config["Url:NotificationWebSocketUrl"]);
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

var monitoringService = app.Services.GetService<IRabitMqService>();

var cancellationTokenSource = new CancellationTokenSource();
var rabbitMqConsumerThread = new Thread(() => monitoringService.ListenToSensorQueue(cancellationTokenSource.Token));
rabbitMqConsumerThread.Start();
IHostApplicationLifetime lifetime = app.Lifetime;

lifetime.ApplicationStopping.Register(() =>
{
    cancellationTokenSource.Cancel();
    rabbitMqConsumerThread.Join();
});

app.Run();