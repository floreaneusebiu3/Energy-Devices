namespace DeviceManagementService.Exceptions
{
    public class UserDeviceNotFoundException : CustomException
    {
        public UserDeviceNotFoundException(string message) : base(message)
        {
        }
    }
}
