using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SecurionPay.Common;
using SecurionPay.Enums;
using System;
using System.Collections.Generic;


namespace SecurionPay.Request
{
    public class ChargeFlowRequest : BaseRequest
    {
        [JsonProperty("returnUrl")]    
        public String ReturnUrl { get; set; }

    }
}
