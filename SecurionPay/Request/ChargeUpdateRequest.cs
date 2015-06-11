using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
namespace SecurionPay.Request
{
    public class ChargeUpdateRequest
    {
        [JsonIgnore]
        public String ChargeId { get;  set; }

        [JsonProperty("customerId")]
        public String CustomerId { get; set; }

        [JsonProperty("description")]
        public String Description { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<String, String> Metadata { get; set; }

        [JsonExtensionData]
        public IDictionary<string, JToken> Other;
    }
}
