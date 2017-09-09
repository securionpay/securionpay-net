using Newtonsoft.Json;
using SecurionPay.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Request
{
    public class DisputeUpdateRequest
    {
        [JsonIgnore]
        public string DisputeId { get; set; }

        [JsonProperty("evidence")]
        public DisputeEvidence Evidence { get; set; }
    }
}
