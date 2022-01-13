using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SecurionPay.Common;
using SecurionPay.Enums;
using System;
using System.Collections.Generic;
namespace SecurionPay.Request
{
    public class ChargeFraudDetailRequest : BaseRequest
    {
        [JsonProperty("status")]
        public FraudStatus Status { get; set; }

    }
}
