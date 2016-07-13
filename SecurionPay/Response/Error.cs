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
        public string Message { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(SafeEnumConverter))]
        public ErrorType Type { get; set; }

        [JsonProperty("code")]
        [JsonConverter(typeof(SafeEnumConverter))]
        public ErrorCode? Code { get; set; }

        [JsonProperty("chargeId")]
        public string ChargeId { get; set; }

        [JsonProperty("blacklistRuleId")]
        public string BlacklistRuleId { get; set; }

    }
}
