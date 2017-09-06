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
        public long Start { get; set; }

        [JsonProperty("currentPeriodStart")]
        public long CurrentPeriodStart { get; set; }

        [JsonProperty("currentPeriodEnd")]
        public long CurrentPeriodEnd { get; set; }

        [JsonProperty("canceledAt")]
        public long CanceledAt { get; set; }

        [JsonProperty("endedAt")]
        public long EndedAt { get; set; }

        [JsonProperty("trialStart")]
        public long TrialStart { get; set; }

        [JsonProperty("trialEnd")]
        public long TrialEnd { get; set; }

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