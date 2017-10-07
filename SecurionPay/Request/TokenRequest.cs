using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
namespace SecurionPay.Request
{
    public class TokenRequest : BaseRequest
    {
        [JsonProperty("number")]
        public String Number { get; set; }

        [JsonProperty("expMonth")]
        public String ExpMonth { get; set; }

        [JsonProperty("expYear")]
        public String ExpYear { get; set; }

        [JsonProperty("cvc")]
        public String Cvc { get; set; }

        [JsonProperty("cardholderName")]
        public String CardholderName { get; set; }

        [JsonProperty("addressLine1")]
        public String AddressLine1 { get; set; }

        [JsonProperty("addressLine2")]
        public String AddressLine2 { get; set; }

        [JsonProperty("addressCity")]
        public String AddressCity { get; set; }

        [JsonProperty("AddressState")]
        public String AddressState { get; set; }

        [JsonProperty("addressZip")]
        public String AddressZip { get; set; }

        [JsonProperty("addressCountry")]
        public String AddressCountry { get; set; }

        [JsonProperty("fraudCheckData")]
        public FraudCheckDataRequest FraudCheckData { get; set; }
    }
}
