using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SecurionPay.Converters;
using SecurionPay.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Response
{

    public class Plan
    {
        [JsonProperty("id")]
        public String Id { get; set; }

        [JsonProperty("created")]
        [JsonConverter(typeof(DateConverter))]
        public DateTime Created { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("currency")]
        public String Currency { get; set; }

        [JsonProperty("interval")]
        public Interval Interval { get; set; }

        [JsonProperty("intervalCount")]
        public int IntervalCount { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("trialPeriodDays")]
        public int TrialPeriodDays { get; set; }

        [JsonProperty("statementDescription")]
        public String StatementDescription { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<String, String> Metadata { get; set; }

        [JsonProperty("recursTo")]
        public String RecursTo { get; set; }

        [JsonProperty("billingCycles")]
        public int BillingCycles { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonExtensionData]
        public IDictionary<string, JToken> Other;
    }
}