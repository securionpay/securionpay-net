using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
namespace SecurionPay.Request
{
    public class RefundRequest
    {
        [JsonIgnore]
        public String ChargeId { get;  set; }

        [JsonProperty("amount")]
        public int? Amount { get; set; }

        [JsonExtensionData]
        public IDictionary<string, JToken> Other;
    }
}
