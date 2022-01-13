using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SecurionPay.Enums
{
    public enum AuthenticationFlow
    {
        [EnumMember(Value="frictionless")]
        frictionless,
        [EnumMember(Value="challenge")]
        Challenge,

        // Used when received value can't be mapped to this enumeration.
        Unrecognized

    }
}
