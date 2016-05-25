using Newtonsoft.Json;
using SecurionPay.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Response
{
    public class CustomerRecord
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("created")]
        [JsonConverter(typeof(DateConverter))]
        public DateTime Created { get; set; }

        [JsonProperty("updated")]
        [JsonConverter(typeof(DateConverter))]
        public DateTime Updated { get; set; }

        [JsonProperty("subscription")]
        public bool Subscription { get; set; }

        [JsonProperty("latest")]
        public bool Latest { get; set; }

        [JsonProperty("volume")]
        public int Volume { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("chargesCount")]
        public int ChargesCount { get; set; }

        [JsonProperty("refundsCount")]
        public int RefundsCount { get; set; }

        [JsonProperty("chargebacksCount")]
        public int ChargebacksCount { get; set; }

        [JsonProperty("verifiedPhone")]
        public bool VerifiedPhone { get; set; }
    }
}
