using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
namespace SecurionPay.Request
{
    public class CaptureRequest : BaseRequest
    {
        [JsonIgnore]
        public String ChargeId { get; set; }
    }
}
