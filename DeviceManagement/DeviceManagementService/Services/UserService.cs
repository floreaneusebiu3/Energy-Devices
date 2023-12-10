using DeviceManagementService.Common;
using DeviceManagementService.Exceptions;
using DeviceManagementService.Interfaces;
using DeviceManagementService.Mappers.UserMappers;
using DeviceManagementService.Mappers.UserMappers.Dtos;
using DevicesManagementData.Repositories;
using DevicesManagementDomain;

namespace DeviceManagementService
{
    public class UserService : IUserService
    {
        private readonly BaseRepository<User> _userRepository;

        public UserService(BaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public Response<string> Add(UserDto userDto)
        {
            _userRepository.Add(userDto.ProjectToEntity());
            _userRepository.SaveChanges();
            return new Response<string>("added");
        }

        public Response<string> Update(UserDto userDto, Guid id)
        {
            var user = _userRepository.Query(u => u.Id.Equals(id))
                .FirstOrDefault();
            if (user == null)
            {
                return new Response<string>(new UserNotFoundException(Constants.USER_NOT_FOUND));
            }
            user.Name = userDto.Name;
            user.Email = userDto.Email;
            _userRepository.SaveChanges();
            return new Response<string>("updated");
        }

        public Response<string> Delete(Guid id)
        {
            var user = _userRepository.Query(u => u.Id.Equals(id))
                .FirstOrDefault();
            if (user == null)
            {
                return new Response<string>(new UserNotFoundException(Constants.USER_NOT_FOUND));
            }
            _userRepository.Remove(user);
            _userRepository.SaveChanges();
            return new Response<string>("deleted");
        }
    }
}
