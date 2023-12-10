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
        private string baseUrl;
        private readonly IConfiguration _config;
        private string token;

        public ApiClient(IConfiguration config)
        {
            _config = config;
            token = _config.GetValue<string>("JWT:NeverExpireToken");
            baseUrl = _config.GetValue<string>("Url:ApiClientBaseUrl");
        }

        public async Task<HttpResponseMessage> AddUserInDevices(RegisterUserDto registerUserDto, Guid id)
        {
            var url = baseUrl + "/devices-users";
            var user = new UserGatewayDto
            {
                Id = id,
                Name = registerUserDto.Name,
                Email = registerUserDto.Email,
            };
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await httpClient.PostAsync(url, data);
        }

        public async Task<HttpResponseMessage> UpdateUserInDevices(UserDto userDto, Guid id)
        {
            var url = baseUrl + "/devices-users/" + id.ToString();
            var user = new UserGatewayDto
            {
                Id = id,
                Name = userDto.Name,
                Email = userDto.Email
            };
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await httpClient.PutAsync(url, data);
        }

        public async Task<HttpResponseMessage> DeleteUserInDevices(Guid id)
        {
            var url = baseUrl + "/devices-users/" + id.ToString();
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await httpClient.DeleteAsync(url);
        }
    }
}
