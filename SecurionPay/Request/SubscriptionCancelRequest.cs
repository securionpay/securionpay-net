using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Request
{
    public class SubscriptionCancelRequest : BaseRequest
    {
        [JsonIgnore]
        public String SubscriptionId;

        [JsonProperty("atPeriodEnd")]
        public bool? AtPeriodEnd;

    }
}
