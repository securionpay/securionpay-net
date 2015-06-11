using SecurionPay.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Exception
{
    public class SecurionPayException : System.Exception
    {
        public SecurionPayException(Error error)
        {
            Error = error;
        }

        public Error Error{ get; private set; }
    }
}
