namespace ChatManagementService.Exceptions;

public class UserAlreadyInGroupException : CustomException
{
    public UserAlreadyInGroupException(string message) : base(message)
    {
    }
}
