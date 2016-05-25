using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SecurionPay.Request.CrossSaleOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Request
{
    public class CheckoutRequest
    {
        [JsonProperty("charge")]
        public CrossSaleOfferRequestCharge Charge;

        [JsonProperty("subscription")]
        public CrossSaleOfferRequestSubscription Subscription;

        [JsonProperty("customerId")]
        public string CustomerId;

        [JsonProperty("crossSaleOfferIds")]
        public List<String> CrossSaleOfferIds;

        [JsonProperty("rememberMe")]
        public bool RememberMe;

        [JsonExtensionData]
        public IDictionary<string, JToken> Other;
    }
}
