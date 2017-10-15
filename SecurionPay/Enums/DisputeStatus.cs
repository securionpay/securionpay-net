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

        [EnumMember(Value = "RETRIEVAL_REQUEST_RESPONSE_UNDER_REVIEW")]
        RetrievalRequestResponseUnderReview,

        [EnumMember(Value = "CHARGEBACK_RESPONSE_UNDER_REVIEW")]
        ChargebackResponseUnderReview,

        [EnumMember(Value = "CHARGEBACK_REPRESENTED_SUCCESSFULLY")]
        ChargebackRepresentedSuccessfully,

        [EnumMember(Value = "CHARGEBACK_REPRESENTED_UNSUCCESSFULLY")]
        ChargebackRepresentedUnsuccessfully
    }
}
