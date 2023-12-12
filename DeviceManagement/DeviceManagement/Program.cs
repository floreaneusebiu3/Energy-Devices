using DeviceManagementService;
using DeviceManagementService.Interfaces;
using DeviceManagementService.Mappers.Dtos;
using DeviceManagementService.Mappers.UserDeviceDeviceMappers.Dtos;
using DeviceManagementService.Mappers.UserMappers.Dtos;
using DeviceManagementService.Services;
using DeviceManagementService.Services.Interfaces;
using DevicesManagementData;
using DevicesManagementData.Repositories;
using DevicesManagementDomain;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

IConfiguration _config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var AngularSpecificOrigins = "_AllowAngularApp";


builder.Services.AddCors(p =>
{
    p.AddPolicy(MyAllowSpecificOrigins, builder =>
        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
    p.AddPolicy(AngularSpecificOrigins, builder =>
        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()
        );
}
);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DeviceManagementContext>();
RegisterScopedInstances();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("JWT:Key")))
                    };
                });
var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);
app.UseCors(AngularSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

void RegisterScopedInstances()
{
    builder.Services.AddScoped<BaseRepository<Device>>();
    builder.Services.AddScoped<BaseRepository<User>>();
    builder.Services.AddScoped<BaseRepository<UserDevice>>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IUserDeviceService, UserDeviceService>();
    builder.Services.AddScoped<IDeviceService, DeviceService>();
    builder.Services.AddScoped<IValidator<UserDto>, UserDtoValidator>();
    builder.Services.AddScoped<IValidator<DeviceDto>, DeviceDtoValidator>();
    builder.Services.AddScoped<IValidator<UserDevicePostDto>, UserDevicePostDtoValidator>();
    builder.Services.AddSingleton<IRabbitMqProducer, RabbitMqProducer>();
}