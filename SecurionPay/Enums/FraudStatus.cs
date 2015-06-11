using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SecurionPay.Enums
{
    public enum FraudStatus
    {
        [EnumMember(Value = "in_progress")]
        InProgress,

        [EnumMember(Value = "safe")]
        Safe,

        [EnumMember(Value = "suspicious")]
        Suspicious,

        [EnumMember(Value = "fraudulent")]
        Fraudulent,

        [EnumMember(Value = "unknown")]
        Unknown,


        //Used when received value can't be mapped to this enumeration.
        Unrecognized
    }
}
