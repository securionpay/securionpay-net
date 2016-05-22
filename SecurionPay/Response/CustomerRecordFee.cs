using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Response
{
    public class CustomerRecordFee
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("customerRecordId")]
        public string CustomerRecordId { get; set; }
    }
}
