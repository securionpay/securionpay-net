using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SecurionPay.Common;
using SecurionPay.Converters;
using System;
using System.Collections.Generic;
namespace SecurionPay.Request
{
    public class ChargeRequest : BaseRequest
    {
        [JsonProperty("amount")]
        public int? Amount { get; set; }

        /// <summary>
        /// Currency ISO Code
        /// </summary>
        [JsonProperty("currency")]
        public String Currency { get; set; }

        [JsonProperty("description")]
        public String Description { get; set; }

        [JsonProperty("customerId")]
        public String CustomerId { get; set; }

        [JsonProperty("captured")]
        public bool? Captured { get; set; }

        [JsonProperty("billing")]
        public Billing Billing { get; set; }

        [JsonProperty("shipping")]
        public Shipping Shipping { get; set; }

        [JsonProperty("threeDSecure")]
        public ThreeDSecure ThreeDSecure { get; set; }

        [JsonProperty("card")]
        public CardRequest Card { get; set; }

        [JsonProperty("paymentMethod")]
        public PaymentMethodRequest paymentMethod { get; set; }

        [JsonProperty("flow")]
        public ChargeFlowRequest flow { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<String, String> Metadata { get; set; }
    }
}
