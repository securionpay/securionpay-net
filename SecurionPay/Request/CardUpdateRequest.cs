using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SecurionPay.Response;
using System;
using System.Collections.Generic;
namespace SecurionPay.Request
{
    public class CardUpdateRequest : BaseRequest
    {
        [JsonIgnore]
        public String CardId { get; set; }

        [JsonIgnore]
        public String CustomerId { get; set; }

        [JsonProperty("expMonth")]
        public String ExpMonth { get; set; }

        [JsonProperty("expYear")]
        public String ExpYear { get; set; }

        [JsonProperty("cardholderName")]
        public String CardholderName { get; set; }

        [JsonProperty("addressCountry")]
        public String AddressCountry { get; set; }

        [JsonProperty("addressCity")]
        public String AddressCity { get; set; }

        [JsonProperty("addressState")]
        public String AddressState { get; set; }

        [JsonProperty("addressZip")]
        public String AddressZip { get; set; }

        [JsonProperty("addressLine1")]
        public String AddressLine1 { get; set; }

        [JsonProperty("addressLine2")]
        public String AddressLine2 { get; set; }

    }
}
