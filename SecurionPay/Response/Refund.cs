using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Response
{
    public class Refund
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("currency")]
        public String Currency { get; set; }

        [JsonProperty("created")]
        public long Created { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }
    }
}

