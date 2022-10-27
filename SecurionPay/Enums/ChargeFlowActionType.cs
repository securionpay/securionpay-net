using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SecurionPay.Enums
{
    public enum  ChargeFlowActionType
    {
        [EnumMember(Value = "redirect")]
        Redirect,

        [EnumMember(Value = "wait")]
        Wait,

        [EnumMember(Value = "none")]
        None,
    }
}
