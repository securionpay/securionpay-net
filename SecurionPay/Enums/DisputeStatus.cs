using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SecurionPay.Enums
{
    public enum DisputeStatus
    {
        [EnumMember(Value = "RETRIEVAL_REQUEST_NEW")]
        NewRetrievalRequest,

        [EnumMember(Value = "RETRIEVAL_REQUEST_REPRESENTED")]
        RetrievalRequestRepresented,

        [EnumMember(Value = "CHARGEBACK_NEW")]
        NewChargeback,

        [EnumMember(Value = "CHARGEBACK_REPRESENTED_SUCCESSFULLY")]
        ChargebackRepresentedSuccessfully,

        [EnumMember(Value = "CHARGEBACK_REPRESENTED_UNSUCCESSFULLY")]
        ChargebackRepresentedUnsuccessfully
    }
}
