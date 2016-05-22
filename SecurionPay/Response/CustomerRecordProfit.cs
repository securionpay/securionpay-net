using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Response
{
    public class CustomerRecordProfit
    {
        [JsonProperty("id")]
        private string Id { get; set; }

        [JsonProperty("created")]
        private DateTime Created { get; set; }

        [JsonProperty("amount")]
        private int Amount { get; set; }

        [JsonProperty("currency")]
        private string Currency { get; set; }

        [JsonProperty("customerRecordId")]
        private string CustomerRecordId { get; set; }
    }
}
