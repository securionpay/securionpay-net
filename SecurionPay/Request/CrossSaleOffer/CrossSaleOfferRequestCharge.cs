using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Request.CrossSaleOffer
{
    public class CrossSaleOfferRequestCharge
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("capture")]
        public bool Capture { get; set; }

        [JsonExtensionData]
        public IDictionary<string, JToken> Other;
    }
}
