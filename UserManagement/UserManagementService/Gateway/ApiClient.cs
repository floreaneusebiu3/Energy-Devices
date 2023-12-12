using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using UserManagement.Controllers.Dtos;
using UserManagementService.Gateway.Dtos;

namespace UserManagementService.Gateway
{
    public class ApiClient
    {
        private string devicesBaseUrl;
        private string chatBaseUrl;
        private readonly IConfiguration _config;
        private string token;

        public ApiClient(IConfiguration config)
        {
            _config = config;
            token = _config.GetValue<string>("JWT:NeverExpireToken");
            devicesBaseUrl = _config.GetValue<string>("Url:DevicesClientBaseUrl");
            chatBaseUrl = _config.GetValue<string>("Url:ChatClientBaseUrl");
        }

        public async Task<HttpResponseMessage> AddUserInDevices(RegisterUserDto registerUserDto, Guid id)
        {
            var devicesUrl = devicesBaseUrl + "/devices-users";
            var chatUrl = chatBaseUrl + "/User";
            
            var devicesUser = new DevicesUserGatewayDto
            {
                Id = id,
                Name = registerUserDto.Name,
                Email = registerUserDto.Email,
            };

            var chatUser = new ChatUserGatewayDto
            {
                Id = id,
                Name = registerUserDto.Name,
                Role = registerUserDto.Role,
            };

            var json1 = JsonConvert.SerializeObject(devicesUser);
            var devicesData = new StringContent(json1, Encoding.UTF8, "application/json");
            var json2 = JsonConvert.SerializeObject(chatUser);
            var chatData = new StringContent(json2, Encoding.UTF8, "application/json");

            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            await httpClient.PostAsync(chatUrl, chatData);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            await httpClient.PostAsync(devicesUrl, devicesData);
            return new HttpResponseMessage();
        }

        public async Task<HttpResponseMessage> UpdateUserInDevices(UserDto userDto, Guid id)
        {
            var devicesUrl = devicesBaseUrl + "/devices-users/" + id.ToString();
            var chatUrl = chatBaseUrl + $"/User";

            var devicesUser = new DevicesUserGatewayDto
            {
                Id = id,
                Name = userDto.Name,
                Email = userDto.Email
            };

            var chatUser = new ChatUserGatewayDto
            {
                Id = id,
                Name = userDto.Name,
                Role = userDto.Role,
            };

            var json1 = JsonConvert.SerializeObject(devicesUser);
            var json2 = JsonConvert.SerializeObject(chatUser);
            var devicesData = new StringContent(json1, Encoding.UTF8, "application/json");
            var chatData = new StringContent(json2, Encoding.UTF8, "application/json");

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var a = await httpClient.PutAsync(chatUrl, chatData);
            var b = await httpClient.PutAsync(devicesUrl, devicesData);
            return new HttpResponseMessage();
        }

        public async Task<HttpResponseMessage> DeleteUserInDevices(Guid id)
        {
            var devicesUrl = devicesBaseUrl + "/devices-users/" + id.ToString();
            var chatUrl = chatBaseUrl + "/User/" + id.ToString();

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            await httpClient.DeleteAsync(chatUrl);
            return await httpClient.DeleteAsync(devicesUrl);
        }
    }
}
