using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SecurionPay.Enums
{
    public enum ErrorCode
    {
        [EnumMember(Value = "no_error")]
        NoError,

        [EnumMember(Value = "invalid_number")]
        InvalidNumber,

	    [EnumMember(Value = "invalid_expiry_month")]
        InvalidExpiryMonth,

	    [EnumMember(Value = "invalid_expiry_year")]
        InvalidExpiryYear,

	    [EnumMember(Value = "invalid_cvc")]
        InvalidCvc,

	    [EnumMember(Value = "incorrect_cvc")]
        IncorrectCvc,

	    [EnumMember(Value = "incorrect_zip")]
        Incorrect_zip,

	    [EnumMember(Value = "expired_card")]
        Expired_card,

	    [EnumMember(Value = "insufficient_funds")]
        InsufficientFunds,

	    [EnumMember(Value = "lost_or_stolen")]
        LostOrStolen,

	    [EnumMember(Value = "suspected_fraud")]
        SuspectedFraud,

	    [EnumMember(Value = "card_declined")]
        Card_declined,

	    [EnumMember(Value = "processing_error")]
        ProcessingError,

	    [EnumMember(Value = "blacklisted")]
        Blacklisted,

        [EnumMember(Value = "expired_token")]
        ExpiredToken,

	    /**
	     * Used when received value can't be mapped to this enumeration.
	     */
	    Unrecognized
    }
}
