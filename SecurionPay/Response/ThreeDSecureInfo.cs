using Newtonsoft.Json;
using SecurionPay.Converters;
using SecurionPay.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Response
{
    public class ThreeDSecureInfo
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }

        /// <summary>
        /// Currency ISO Code
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("enrolled")]
        public bool Enrolled { get; set; }

        [JsonProperty("liabilityShift")]
        [JsonConverter(typeof(SafeEnumConverter))]
        public LiabilityShift LiabilityShift { get; set; }

    }
}
