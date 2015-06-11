using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using SecurionPay.Converters;
using SecurionPay.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Response
{
    public class Charge
    {

        [JsonProperty("id")]
        public String Id { get; set; }

        [JsonProperty("created")]
        public long Created { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("currency")]
        public String Currency { get; set; }

        [JsonProperty("description")]
        public String Description { get; set; }

        [JsonProperty("card")]
        public Card Card { get; set; }

        [JsonProperty("customerId")]
        public String CustomerId { get; set; }

        [JsonProperty("captured")]
        public Boolean Captured { get; set; }

        [JsonProperty("refunded")]
        public Boolean Refunded { get; set; }

        [JsonProperty("refunds")]
        public List<Refund> Refunds { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<String, String> Metadata { get; set; }

        [JsonProperty("disputed")]
        public bool Disputed { get; set; }

        [JsonProperty("fraudDetails")]
        public FraudDetails FraudDetails { get; set; }

        [JsonProperty("failureCode")]
        [JsonConverter(typeof(SafeEnumConverter))]
        public ErrorCode FailureCode { get; set; }

        [JsonProperty("failureMessage")]
        public string FailureMessage { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonExtensionData]
        public IDictionary<string, JToken> Other;

    }
}


