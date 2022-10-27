using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
namespace SecurionPay.Request
{
    public class CustomerUpdateRequest : BaseRequest
    {
        [JsonIgnore]
        public String CustomerId { get; set; }

        [JsonProperty("email")]
        public String Email { get; set; }

        [JsonProperty("description")]
        public String Description { get; set; }

        [JsonProperty("card")]
        public CardRequest Card { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<String, String> Metadata { get; set; }

        [JsonProperty("defaultCardId")]
        public String DefaultCardId { get; set; }

        [JsonProperty("defaultPaymentMethodId")]
        public String DefaultPaymentMethodId { get; set; }
    }
}
