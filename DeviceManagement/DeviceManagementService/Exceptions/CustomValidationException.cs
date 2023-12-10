namespace DeviceManagementService.Exceptions
{
    public class CustomValidationException : CustomException
    {
        public CustomValidationException(string message) : base(message) { }
    }
}
