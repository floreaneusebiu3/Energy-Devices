using UserManagement.Controllers.Dtos;
using UserManagementData.Repositories;
using UserManagementDomain;
using UserManagementService.Common;
using UserManagementService.Exceptions;
using UserManagementService.Gateway;
using UserManagementService.Interfaces;
using UserManagementService.Mappers;

namespace UserManagementService
{
    public class UserService : IUserService
    {
        private readonly BaseRepository<User> _userRepossitory;
        private readonly UserMapper _userMapper;
        private readonly ApiClient _apiClient;

        public UserService(BaseRepository<User> userRepository, UserMapper userMapper, ApiClient apiClient)
        {
            _userRepossitory = userRepository;
            _userMapper = userMapper;
            _apiClient = apiClient;
        }

        public async Task<Response<UserDto>> Add(RegisterUserDto registerUserDto)
        {
            Guid id = Guid.NewGuid();
            var existingUser = getUserByEmail(registerUserDto.Email);
            if (existingUser != null)
            {
                return new Response<UserDto>(new UserAlreadyExistingException(Constants.EMAIL_TAKEN));
            }
            try
            {
                var a = await _apiClient.AddUserInDevices(registerUserDto, id);
            }
            catch (HttpRequestException)
            {
                return new Response<UserDto>(new BadGatewayException(Constants.BAD_GATEWAY));
            }
            var user = _userMapper.mapRegisterUserDtoToUser(registerUserDto);
            user.Id = id;
            _userRepossitory.Add(user);
            _userRepossitory.SaveChanges();
            var addedUserDto = _userMapper.mapUserToUserDto(user);
            return new Response<UserDto>(addedUserDto);
        }

        public async Task<Response<UserDto>> Update(UserDto userDto, Guid id)
        {
            var user = getUserById(id);
            if (user == null)
            {
                return new Response<UserDto>(new UserNotFoundException(Constants.USER_NOT_FOUND));
            }
            var userWithSameEmail = getUserByEmail(userDto.Email);
            if (userWithSameEmail != null && !user.Equals(userWithSameEmail))
            {
                return new Response<UserDto>(new UserAlreadyExistingException(Constants.EMAIL_TAKEN));
            }
            userDto.Id = id;
            try
            {
                await _apiClient.UpdateUserInDevices(userDto, id);
            }
            catch (HttpRequestException)
            {
                return new Response<UserDto>(new BadGatewayException(Constants.BAD_GATEWAY));
            }
            AssignUpdatedPropertiesToUser(user, userDto);
            var updatedUserDto = _userMapper.mapUserToUserDto(user);
            return new Response<UserDto>(updatedUserDto);
        }

        public async Task<Response<IEnumerable<UserDto>>> FindAll()
        {
            var userList = _userRepossitory.Query()
                 .Select(user => _userMapper.mapUserToUserDto(user))
                 .ToList();
            return new Response<IEnumerable<UserDto>>(userList);
        }

        public async Task<Response<Guid>> Delete(Guid id)
        {
            var user = getUserById(id);
            if (user == null)
            {
                return new Response<Guid>(new UserNotFoundException(Constants.USER_NOT_FOUND));
            }
            try
            {
                await _apiClient.DeleteUserInDevices(id);
            }
            catch (HttpRequestException)
            {
                return new Response<Guid>(new BadGatewayException(Constants.BAD_GATEWAY));
            }
            return await DeleteUser(user);
        }

        private User? getUserByEmail(string email)
        {
            return _userRepossitory
                .Query(u => u.Email.Equals(email)).FirstOrDefault();
        }

        private User? getUserById(Guid id)
        {
            return _userRepossitory
                .Query(u => u.Id.Equals(id)).FirstOrDefault();
        }

        private void AssignUpdatedPropertiesToUser(User user, UserDto userDto)
        {
            user.Id = userDto.Id;
            user.Email = userDto.Email;
            user.Role = userDto.Role.ToUpper();
            user.Name = userDto.Name;
            _userRepossitory.SaveChanges();
        }

        private async Task<Response<Guid>> DeleteUser(User user)
        {
            _userRepossitory.Remove(user);
            _userRepossitory.SaveChanges();
            return new Response<Guid>(user.Id);
        }
    }
}
