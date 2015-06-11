using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Request
{
    public class CreatedFilter
    {
        [JsonProperty("gt")]
        public long? Gt { get; set; }

        [JsonProperty("gte")]
        public long? Gte { get; set; }

        [JsonProperty("lt")]
        public long? Lt { get; set; }

        [JsonProperty("lte")]
        public long? Lte { get; set; }
    }
}
