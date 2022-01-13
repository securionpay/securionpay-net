using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SecurionPay.Enums
{
    public enum ThreeDSecureExternalStatus
    {
        Y, N, A, U, R, E,

        //Used when received value can't be mapped to this enumeration.
        Unrecognized
    }
}
