using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SecurionPay.Enums
{
    public enum LiabilityShift
    {
        [EnumMember(Value = "successful")]
        Successful,

        [EnumMember(Value = "failed")]
        Failed,

        [EnumMember(Value = "not_possible")]
        NotPossible,
    }
}
