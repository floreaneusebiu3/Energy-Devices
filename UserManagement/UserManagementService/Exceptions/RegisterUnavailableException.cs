namespace UserManagementService.Exceptions
{
    public class RegisterUnavailableException : CustomException
    {
        public RegisterUnavailableException(string message) : base(message)
        {
        }
    }
}
