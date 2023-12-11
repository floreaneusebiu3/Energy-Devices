using ChatData;
using ChatDomain;
using ChatManagementData.Repositories;
using ChatManagementService.Gateway;
using ChatManagementService.Model;
using ChatManagementService.Services;
using ChatManagementService.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

// Add services to the container.
RegisterSingleton();
builder.Services.AddDbContext<ChatManagementContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _config.GetValue<string>("JWT:Issuer"),
                        ValidAudience = _config.GetValue<string>("JWT:Audience"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("JWT:Key")))
                    };
                });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var app = builder.Build();

app.UseCors(RabbitMqSpecificOrigins);
app.UseCors(NSwagSpecificOrigins);
app.UseCors(AngularSpecificOrigins);

app.UseWebSockets(new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromSeconds(120),
});

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ClientHub>(_config["Url:ChatWebSocketUrl"]);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

void RegisterSingleton()
{
    builder.Services.AddScoped<BaseRepository<User>>();
    builder.Services.AddScoped<BaseRepository<UserGroup>>();
    builder.Services.AddScoped<BaseRepository<Message>>();
    builder.Services.AddScoped<BaseRepository<Group>>();

    builder.Services.AddScoped<IGroupService, GroupService>();
    builder.Services.AddScoped<IMessageService, MessageService>();
    builder.Services.AddScoped<IUserService, UserService>();

    builder.Services.AddScoped<IValidator<CreateGroupDto>, CreateGroupDtoValidator>();
    builder.Services.AddScoped<IValidator<GroupMessageDto>, GroupMessageDtoValidator>();
    builder.Services.AddScoped<IValidator<UserMessageDto>, UserMessageDtoValidator>();
    builder.Services.AddScoped<IValidator<UserDto>, UserDtoValidator>();
}