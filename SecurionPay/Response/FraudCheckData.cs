using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Response
{
    public class FraudCheckData
    {

        [JsonProperty("ipCountry")]
        public String IpCountry { get; set; }

        [JsonProperty("ipAddress")]
        public String IpAddress { get; set; }

        [JsonProperty("email")]
        public String Email { get; set; }

        [JsonProperty("userAgent")]
        public String UserAgent { get; set; }

        [JsonProperty("acceptLanguage")]
        public String AcceptLanguage { get; set; }

        [JsonExtensionData]
        public IDictionary<string, JToken> Other;
    }
}
