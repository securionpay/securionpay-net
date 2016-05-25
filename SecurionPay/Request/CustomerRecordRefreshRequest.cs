using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Request
{
    public class CustomerRecordRefreshRequest
    {
        [JsonIgnore]
        public string CustomerRecordId { get; set; }

        [JsonProperty("subscription")]
        public bool Subscription { get; set; }
    }
}
