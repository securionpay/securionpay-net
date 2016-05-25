using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Request
{
    public class CustomerRecordListRequest
    {
        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("startingAfterId")]
        public string StartingAfterId { get; set; }

        [JsonProperty("endingBeforeId")]
        public string EndingBeforeId { get; set; }

        [JsonProperty("includeTotalCount")]
        public bool IncludeTotalCount { get; set; }

        [JsonProperty("created")]
        public CreatedFilter Created { get; set; }
    }
}
