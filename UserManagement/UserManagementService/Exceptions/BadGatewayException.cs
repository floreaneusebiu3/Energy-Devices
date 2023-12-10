namespace UserManagementService.Exceptions
{
    public class BadGatewayException : CustomException
    {
        public BadGatewayException(string message) : base(message)
        {
        }
    }
}
