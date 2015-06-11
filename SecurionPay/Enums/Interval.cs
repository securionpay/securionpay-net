using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace SecurionPay.Enums
{

    public enum Interval
    {
        [EnumMember(Value = "day")]
        Day,
        [EnumMember(Value = "week")]
        Week,
        [EnumMember(Value = "month")]
        Month,
        [EnumMember(Value = "year")]
        Year,

        Unrecognized
    }

}