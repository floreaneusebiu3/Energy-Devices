namespace DeviceManagementService.Exceptions
{
    public class CustomException
    {
        public string Message { get; set; }
       
        public CustomException(string message)
        {
            Message = message;
        }
    }
}
