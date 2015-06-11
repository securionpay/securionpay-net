using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
namespace SecurionPay.Request
{
    public class ChargeRequest
    {
        [JsonProperty("metadata")]
        public Dictionary<String, String> Metadata { get; set; }

        [JsonProperty("amount")]
        public int? Amount { get; set; }

        [JsonProperty("currency")]
        public String Currency { get; set; }

        [JsonProperty("description")]
        public String Description { get; set; }

        [JsonProperty("customerId")]
        public String CustomerId { get; set; }

        [JsonProperty("card")]
        public CardRequest Card { get; set; }

        [JsonProperty("captured")]
        public bool? Captured { get; set; }

        [JsonExtensionData]
        public IDictionary<string, JToken> Other;
    }
}
