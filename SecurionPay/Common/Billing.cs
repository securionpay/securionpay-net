using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Common
{
    public class Billing
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("vat")]
        public string Vat { get; set; }
    }
}
