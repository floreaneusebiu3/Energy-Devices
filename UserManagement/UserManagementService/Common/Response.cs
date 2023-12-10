using UserManagementService.Exceptions;

namespace UserManagementService.Common
{
    public class Response<T>
    {
        public Response(T value)
        {
            Value = value;
            isException = false;
        }

        public Response(CustomException exception)
        { 
            isException = true;
            Exceptions = new List<CustomException> { exception};
        }

        public Response(IEnumerable<CustomException> exceptions)
        { 
            isException = true;
            this.Exceptions = exceptions;
        }

        public T? Value { get; set; }
        public bool isException { get; set; }
        public IEnumerable<CustomException>? Exceptions { get; set; }
    }
}
