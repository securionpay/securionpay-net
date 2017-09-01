using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Request.Checkout
{
    public class CheckoutRequestCharge
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("captured")]
        public bool? Captured { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<String, String> Metadata { get; set; }
    }
}
