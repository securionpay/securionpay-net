using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SecurionPay.Enums
{
    public enum ErrorType
    {
        [EnumMember(Value = "invalid_request")]
        InvalidRequest,

        [EnumMember(Value = "card_error")]
        CardError,

        [EnumMember(Value = "gateway_error")]
        GatewayError,

        Unrecognized
    }
}
