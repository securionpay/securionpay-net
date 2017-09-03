using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Request
{
    public class SubscriptionWithCardFromChargeRequest : SubscriptionRequest
    {
        [JsonProperty("card")]
        public string ChargeId { get; set; }
    }
}
