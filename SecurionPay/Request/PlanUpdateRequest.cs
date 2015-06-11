using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
namespace SecurionPay.Request
{
    public class PlanUpdateRequest
    {
        [JsonIgnore]
        public String PlanId { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("statementDescription")]
        public String StatementDescription { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<String, String> Metadata { get; set; }

        [JsonExtensionData]
        public IDictionary<string, JToken> Other;
    }
}
