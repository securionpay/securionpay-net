using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Request.Checkout
{
    public class CheckoutRequestCustomCharge
    {
        [JsonProperty("amountOptions")]
        public List<int> AmountOptions { get; set; }

        [JsonProperty("customAmount")]
        public CustomAmount CustomAmount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("capture")]
        public bool? Capture { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<String, String> Metadata { get; set; }
    }
}
