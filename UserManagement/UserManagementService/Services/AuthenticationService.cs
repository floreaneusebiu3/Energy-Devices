
using UserManagement.Controllers.Dtos;
using UserManagementData.Repositories;
using UserManagementDomain;
using UserManagementService.authentication;
using UserManagementService.Common;
using UserManagementService.Exceptions;
using UserManagementService.Gateway;
using UserManagementService.Mappers;

namespace UserManagementService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly TokenService _tokenService;
        private readonly UserMapper _userMapper;
        private readonly ApiClient _apiClient;

        public AuthenticationService(BaseRepository<User> userRepository, TokenService tokenService,
            UserMapper userMapper, ApiClient apiClient)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _userMapper = userMapper;
            _apiClient = apiClient;
        }

        public Response<string> authenticateUser(LoginDto loginDto)
        {
            var user = _userRepository.Query(user => user.Email == loginDto.Email)
               .FirstOrDefault();
            if (user == null)
            {
                return new Response<string>(new UserNotFoundException(Constants.INCORRECT_CREDENTIALS));
            }
            user = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password) ? user : null;
            return user != null
                   ? new Response<string>(_tokenService.generateToken(user))
                   : new Response<string>(new UserNotFoundException(Constants.INCORRECT_CREDENTIALS));
        }

        public async Task<Response<UserDto>> RegisterUser(RegisterUserDto registerUserDto)
        {
            Guid id = Guid.NewGuid();
            var existingUser = getUserByEmail(registerUserDto.Email);
            if (existingUser != null)
            {
                return new Response<UserDto>(new UserAlreadyExistingException(Constants.EMAIL_TAKEN));
            }
            var user = _userMapper.mapRegisterUserDtoToUser(registerUserDto);
            user.Id = id;
            try
            {
                await _apiClient.AddUserInDevices(registerUserDto, id);
            }
            catch (HttpRequestException)
            {
                return new Response<UserDto>(new RegisterUnavailableException(Constants.REGISTER_UNAVAILABLE));
            }
            _userRepository.Add(user);
            _userRepository.SaveChanges();
            var addedUserDto = _userMapper.mapUserToUserDto(user);
            return new Response<UserDto>(addedUserDto);
        }

        private User? getUserByEmail(string email)
        {
            return _userRepository
                .Query(u => u.Email.Equals(email)).FirstOrDefault();
        }
    }
}
