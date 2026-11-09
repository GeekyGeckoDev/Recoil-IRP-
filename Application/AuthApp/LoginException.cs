using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AuthApp.Exceptions
{
    public class LoginException : Exception
    {
        //public LoginException(string message) : base(message) { }

        public LoginException(string message)
            : base() { }

       

       
    }
}
