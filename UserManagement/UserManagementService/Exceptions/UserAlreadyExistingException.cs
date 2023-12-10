using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementService.Exceptions
{
    public class UserAlreadyExistingException : CustomException
    {
        public UserAlreadyExistingException(string message) : base(message) { }
    }
}
