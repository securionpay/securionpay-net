using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Request
{
    public class SubscriptionListRequest : ListRequest
    {
        [JsonIgnore]
        public String CustomerId { get; set; }

        [JsonProperty("deleted")]
        public bool? Deleted { get; set; }
    }
}
