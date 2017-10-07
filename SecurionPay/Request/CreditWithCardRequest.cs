using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Request
{
    public class CreditWithCardRequest : BaseRequest
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("card")]
        public CardRequest Card { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("customerId")]
        public string CustomerId { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<String, String> Metadata { get; set; }
    }
}
