using ChatData;
using ChatDomain;
using ChatManagementData.Repositories;
using ChatManagementService.Model;
using ChatManagementService.Services;
using ChatManagementService.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var AngularSpecificOrigins = "_AllowAngularApp";

IConfiguration _config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddCors(p =>
{
    p.AddPolicy(MyAllowSpecificOrigins, builder =>
        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
    p.AddPolicy(AngularSpecificOrigins, builder =>
        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()
        );
}
);

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