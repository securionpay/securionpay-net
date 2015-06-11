using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SecurionPay.Enums
{
    public enum SubscriptionStatus
    {
        [EnumMember(Value = "trialing")]
        Trialing,
        [EnumMember(Value = "active")]
        Active,
        [EnumMember(Value = "past_due")]
        PastDue,
        [EnumMember(Value = "canceled")]
        Canceled,
        [EnumMember(Value = "unpaid")]
        Unpaid,

        /**
         * Used when received value can't be mapped to this enumeration.
         */
        Unrecognized
    }
}
