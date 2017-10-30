using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Common
{
    public class Address
    {
        [JsonProperty("line1")]
        public string FirstLine { get; set; }

        [JsonProperty("line2")]
        public string SecondLine { get; set; }

        [JsonProperty("zip")]
        public string ZipCode { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public string CountryISOCode { get; set; }
    }
}
