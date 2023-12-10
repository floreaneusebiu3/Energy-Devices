using ChatDomain;
using ChatManagementData.Repositories;
using ChatManagementService.Common;
using ChatManagementService.Exceptions;
using ChatManagementService.Mappers;
using ChatManagementService.Model;
using ChatManagementService.Services.Interfaces;

namespace ChatManagementService.Services;

public class UserService : IUserService
{
    private readonly BaseRepository<User> _userRepository;

    public UserService(BaseRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public Response<UserDto> Create(UserDto userDto)
    {
        var user = userDto.MapToEntity();

        _userRepository.Add(user);
        _userRepository.SaveChanges();

        return new Response<UserDto>(user.MapToDto());
    }

    public Response<UserDto> Update(UserDto userDto)
    {
        var user = _userRepository.Query(u => u.Id == userDto.Id)
            .FirstOrDefault();

        if (user is null)
        {
            return new Response<UserDto>(new UserNotFoundException(Constants.API_USER_NOT_FOUND_EXCEPTION));
        }

        user.Name = userDto.Name;
        user.Role = userDto.Role;
        _userRepository.SaveChanges();

        return new Response<UserDto>(user.MapToDto());
    }

    public Response<UserDto> Delete(Guid UserId)
    {
        var user = _userRepository.Query(u => u.Id == UserId)
            .FirstOrDefault();

        if (user is null)
        {
            return new Response<UserDto>(new UserNotFoundException(Constants.API_USER_NOT_FOUND_EXCEPTION));
        }

        _userRepository.Remove(user);
        _userRepository.SaveChanges();

        return new Response<UserDto>(user.MapToDto());
    }

    public Response<List<UserDto>> ReadAll()
    {
        var users =  _userRepository.Query()
                        .Select(u => u.MapToDto())
                        .ToList();
        return new Response<List<UserDto>>(users);
    }
}
