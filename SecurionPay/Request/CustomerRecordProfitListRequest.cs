using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Request
{
    public class CustomerRecordProfitListRequest : ListRequest
    {
        [JsonIgnore]
        public string CustomerRecordId { get; set; }
    }
}
