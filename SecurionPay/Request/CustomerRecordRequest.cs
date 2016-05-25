using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Request
{
    public class CustomerRecordRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("subscription")]
        public bool Subscription { get; set; }
    }
}
