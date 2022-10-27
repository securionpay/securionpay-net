using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SecurionPay.Enums
{
    public enum ChargeStatus
    {
        [EnumMember(Value = "successful")]
        Successful,

        [EnumMember(Value = "pending")]
        Pending,

        [EnumMember(Value ="failed")]
        Failed,
    }
}
