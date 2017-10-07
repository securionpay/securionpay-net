using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
namespace SecurionPay.Request
{
    public class RefundRequest : BaseRequest
    {
        [JsonIgnore]
        public String ChargeId { get;  set; }

        [JsonProperty("amount")]
        public int? Amount { get; set; }

    }
}
