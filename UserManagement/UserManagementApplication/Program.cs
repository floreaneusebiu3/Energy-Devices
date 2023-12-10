using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserManagement.Controllers.Dtos;
using UserManagementData;
using UserManagementData.Repositories;
using UserManagementDomain;
using UserManagementService;
using UserManagementService.authentication;
using UserManagementService.Gateway;
using UserManagementService.Interfaces;
using UserManagementService.Mappers;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

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
        );}
);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UserManagementContext>();
RegisterScopedInstances();

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
    builder.Services.AddScoped<BaseRepository<User>>();
    builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
    builder.Services.AddScoped<TokenService, JwtService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<UserMapper>();
    builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
    builder.Services.AddScoped<IValidator<LoginDto>, LoginDtoValidator>();
    builder.Services.AddScoped<IValidator<UserDto>, UserDtoValidator>();
    builder.Services.AddSingleton<ApiClient>();
}