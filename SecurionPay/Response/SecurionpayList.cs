using Newtonsoft.Json;
using System.Collections.Generic;
namespace SecurionPay.Response
{
    public class SecurionpayList
    {

        [JsonProperty("list")]
        public List<object> List { get; set; }

        [JsonProperty("totalCount")]
        public int? TotalCount { get; set; }

    }
}
