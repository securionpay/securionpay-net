using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SecurionPay.Converters;
using SecurionPay.Enums;
using System;
using System.Collections.Generic;
namespace SecurionPay.Request
{
    public class PlanRequest : BaseRequest
    {
        [JsonProperty("amount")]
        public int? Amount { get;  set; }

        [JsonProperty("currency")]
        public String Currency { get; set; }

        [JsonProperty("interval")]
        [JsonConverter(typeof(SafeEnumConverter))]
        public Interval Interval { get; set; }

        [JsonProperty("intervalCount")]
        public int? IntervalCount { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("trialPeriodDays")]
        public int? TrialPeriodDays { get; set; }

        [JsonProperty("statementDescription")]
        public String StatementDescription { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<String, String> Metadata { get; set; }

        [JsonProperty("billingCycles")]
        public int? BillingCycles { get; set; }

        [JsonProperty("recursTo")]
        public String RecursTo { get; set; }

    }
}
