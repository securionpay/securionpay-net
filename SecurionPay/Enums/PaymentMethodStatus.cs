using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SecurionPay.Enums
{
    public enum PaymentMethodStatus
    {
        [EnumMember(Value = "used")]
        Used,

        [EnumMember(Value = "pending")]
        Pending,

        [EnumMember(Value = "chargeable")]
        Chargeable,

        [EnumMember(Value = "failed")]
        Failed,

    }
}
