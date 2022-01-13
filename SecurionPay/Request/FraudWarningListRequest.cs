using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Request
{
    public class FraudWarningListRequest : ListRequest
    {
        [JsonProperty("chargeId")]
        public string ChargeId { get; set; }
        
        [JsonProperty("actionable")]
        public bool? Actionable { get; set; }
    }
}
