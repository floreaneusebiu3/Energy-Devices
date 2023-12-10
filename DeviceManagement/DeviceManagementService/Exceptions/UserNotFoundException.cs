namespace DeviceManagementService.Exceptions
{
    public class UserNotFoundException : CustomException
    {
        public UserNotFoundException(string message) : base(message)
        {
        }
    }
}
