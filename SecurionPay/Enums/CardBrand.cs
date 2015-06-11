using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SecurionPay.Enums
{
    public enum CardBrand
    {
        Visa,
        [EnumMember(Value="American Express")]
        AmericanExpress,
        [EnumMember(Value = "Master")]
        MasterCard,
        Discover,
        JCB,
        [EnumMember(Value="Diners Club")]
        DinersClub,
        Unknown,

        // Used when received value can't be mapped to this enumeration.

        Unrecognized

    }
}
