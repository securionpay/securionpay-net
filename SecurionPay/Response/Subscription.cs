using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SecurionPay.Common;
using SecurionPay.Converters;
using SecurionPay.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Response
{
    public class Subscription
    {
        [JsonProperty("id")]
        public String Id { get; set; }

        [JsonProperty("created")]
        [JsonConverter(typeof(DateConverter))]
        public DateTime Created { get; set; }

        [JsonProperty("planId")]
        public String PlanId { get; set; }

        [JsonProperty("customerId")]
        public String CustomerId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("captureCharges")]
        public bool CaptureCharges { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(SafeEnumConverter))]
        public SubscriptionStatus Status { get; set; }

        [JsonProperty("start")]
        [JsonConverter(typeof(DateConverter))]
        public DateTime Start { get; set; }

        [JsonProperty("currentPeriodStart")]
        [JsonConverter(typeof(DateConverter))]
        public DateTime CurrentPeriodStart { get; set; }

        [JsonProperty("currentPeriodEnd")]
        [JsonConverter(typeof(DateConverter))]
        public DateTime CurrentPeriodEnd { get; set; }

        [JsonProperty("canceledAt")]
        [JsonConverter(typeof(DateConverter))]
        public DateTime CanceledAt { get; set; }

        [JsonProperty("endedAt")]
        [JsonConverter(typeof(DateConverter))]
        public DateTime EndedAt { get; set; }

        [JsonProperty("trialStart")]
        [JsonConverter(typeof(DateConverter))]
        public DateTime TrialStart { get; set; }

        [JsonProperty("trialEnd")]
        [JsonConverter(typeof(DateConverter))]
        public DateTime TrialEnd { get; set; }

        [JsonProperty("cancelAtPeriodEnd")]
        public bool CancelAtPeriodEnd { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("remainingBillingCycles")]
        public int RemainingBillingCycles { get; set; }

        [JsonProperty("shipping")]
        public Shipping Shipping { get; set; }

        [JsonProperty("billing")]
        public Billing Billing { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<String, String> Metadata { get; set; }
    }
}