using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SecurionPay.Enums
{
    public enum EventType
    {

        [EnumMember(Value = "CHARGE_SUCCEEDED")]
        ChargeSucceeded,
        [EnumMember(Value = "CHARGE_FAILED")]
        ChargeFailed,
        [EnumMember(Value = "CHARGE_UPDATED")]
        ChargeUpdated,
        [EnumMember(Value = "CHARGE_CAPTURED")]
        ChargeCaptured,
        [EnumMember(Value = "CHARGE_REFUNDED")]
        ChargeRefunded,

        [EnumMember(Value = "CHARGE_DISPUTE_CREATED")]
        ChargeDisputeCreated,
        [EnumMember(Value = "CHARGE_DISPUTE_UPDATED")]
        ChargeDisputeUpdated,
        [EnumMember(Value = "CHARGE_DISPUTE_WON")]
        ChargeDisputeWon,
        [EnumMember(Value = "CHARGE_DISPUTE_LOST")]
        ChargeDisputeLost,

        [EnumMember(Value = "CHARGE_DISPUTE_FUNDS_WITHDRAWN")]
        ChargeDisputeFundsWithdrawn,
        [EnumMember(Value = "CHARGE_DISPUTE_FUNDS_RESTORED")]
        ChargeDisputeFundsRestored,

        [EnumMember(Value = "CUSTOMER_CARD_CREATED")]
        CustomerCardCreated,
        [EnumMember(Value = "CUSTOMER_CARD_UPDATED")]
        CustomerCardUpdated,
        [EnumMember(Value = "CUSTOMER_CARD_DELETED")]
        CustomerCardDeleted,
        [EnumMember(Value = "CUSTOMER_CREATED")]
        CustomerCreated,
        [EnumMember(Value = "CUSTOMER_UPDATED")]
        CustomerUpdated,
        [EnumMember(Value = "CUSTOMER_DELETED")]
        CustomerDeleted,
        [EnumMember(Value = "CUSTOMER_SUBSCRIPTION_CREATED")]
        CustomerSubscriptionCreated,
        [EnumMember(Value = "CUSTOMER_SUBSCRIPTION_UPDATED")]
        CustomerSubscriptionUpdated,
        [EnumMember(Value = "CUSTOMER_SUBSCRIPTION_DELETED")]
        CustomerSubscriptionDeleted,

        [EnumMember(Value = "PLAN_CREATED")]
        PlanCreated,
        [EnumMember(Value = "PLAN_UPDATED")]
        PlanUpdated,
        [EnumMember(Value = "PLAN_DELETED")]
        PlanDeleted,


        //Used when received value can't be mapped to this enumeration.
        Unrecognized
    }
}
