using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SecurionPay.Converters;
using SecurionPay.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Response
{
    public class BlacklistRule
    {

        [JsonProperty("id")]
        public String Id { get; set; }

        [JsonProperty("created")]
        [JsonConverter(typeof(DateConverter))]
        public DateTime Created { get; set; }

        [JsonProperty("ruleType")]
        [JsonConverter(typeof(SafeEnumConverter))]
        public BlacklistRuleType RuleType { get; set; }


        [JsonProperty("fingerprint")]
        public String Fingerprint { get; set; }

        [JsonProperty("ipAddress")]
        public String IpAddress { get; set; }

        [JsonProperty("ipCountry")]
        public String IpCountry { get; set; }

        [JsonProperty("metadataKey")]
        public String MetadataKey { get; set; }

        [JsonProperty("metadataValue")]
        public String MetadataValue { get; set; }

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
