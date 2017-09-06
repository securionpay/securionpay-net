using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
namespace SecurionPay.Request
{
    public class SubscriptionRequest
    {
        [JsonIgnore]
        public String CustomerId { get; set; }

        [JsonProperty("planId")]
        public String PlanId { get; set; }

        [JsonProperty("quantity")]
        public int? Quantity { get; set; }

        [JsonProperty("trialEnd")]
        public long? TrialEnd { get; set; }

        [JsonProperty("card")]
        public CardRequest Card { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<String, String> Metadata { get; set; }

        [JsonExtensionData]
        public IDictionary<string, JToken> Other;
    }
}
