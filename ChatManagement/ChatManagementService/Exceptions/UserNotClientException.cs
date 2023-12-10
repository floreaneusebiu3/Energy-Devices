namespace ChatManagementService.Exceptions;

public class UserNotClientException : CustomException
{
    public UserNotClientException(string message) : base(message)
    {
    }
}
