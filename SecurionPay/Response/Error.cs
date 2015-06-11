using Newtonsoft.Json;
using SecurionPay.Converters;
using SecurionPay.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Response
{
    public class Error
    {
        [JsonProperty("message")]
        public String Message { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(SafeEnumConverter))]
        public ErrorType Type { get; set; }

        [JsonProperty("code")]
        [JsonConverter(typeof(SafeEnumConverter))]
        public ErrorCode? Code { get; set; }

        [JsonProperty("chargeId")]
        public String ChargeId { get; set; }

        [JsonProperty("blacklistRuleId")]
        public String BlacklistRuleId { get; set; }

    }
}
