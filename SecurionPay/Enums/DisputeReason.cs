using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SecurionPay.Enums
{
    public enum DisputeReason
    {
        [EnumMember(Value = "FRAUDULENT")]
        Fraudulent,

        [EnumMember(Value = "UNRECOGNIZED")]
        Unrecognized,

        [EnumMember(Value = "DUPLICATE")]
        Duplicate,

        [EnumMember(Value = "SUBSCRIPTION_CANCELED")]
        DubscriptionCanceled,

        [EnumMember(Value = "PRODUCT_NOT_RECEIVED")]
        ProductNotReceived,

        [EnumMember(Value = "PRODUCT_UNACCEPTABLE")]
        ProductUnacceptable,

        [EnumMember(Value = "CREDIT_NOT_PROCESSED")]
        CreditNotProcessed,

        [EnumMember(Value = "GENERAL")]
        General,
    }
}
