using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SecurionPay.Converters;
using SecurionPay.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Response
{
    public class FraudDetails
    {

        [JsonProperty("status")]
        [JsonConverter(typeof(SafeEnumConverter))]
        public FraudStatus Status { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }
    }
}
