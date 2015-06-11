using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Request
{
    public class SubscriptionCancelRequest
    {
        [JsonIgnore]
        public String SubscriptionId;
        [JsonIgnore]
        public String CustomerId;

        [JsonProperty("atPeriodEnd")]
        public bool? AtPeriodEnd;

        [JsonExtensionData]
        public IDictionary<string, JToken> Other;
    }
}
