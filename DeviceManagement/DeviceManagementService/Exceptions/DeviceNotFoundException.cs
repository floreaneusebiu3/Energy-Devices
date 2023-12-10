namespace DeviceManagementService.Exceptions
{
    public class DeviceNotFoundException : CustomException
    {
        public DeviceNotFoundException(string? message) : base(message) { }
    }
}
