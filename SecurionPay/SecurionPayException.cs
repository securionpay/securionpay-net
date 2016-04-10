using SecurionPay.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Exception
{
    public class SecurionPayException : System.Exception
    {
        public SecurionPayException(Error error, string requestType, string requestAction)
        {
            Error = error;
            RequestType = requestType;
            RequestAction = requestAction;
        }

        public Error Error{ get; private set; }
        public string RequestType { get; private set; }
        public string RequestAction { get; private set; }
    }
}
