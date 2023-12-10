namespace ChatManagementService.Exceptions;

public class GroupNotFoundException : CustomException
{
    public GroupNotFoundException(string message) : base(message)
    {
    }
}
