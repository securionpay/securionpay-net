using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using SecurionPay.Common;
using SecurionPay.Converters;
using SecurionPay.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Response
{
    public class ChargeFlow : BaseResponse
    {
        [JsonProperty("nextAction")]
        [JsonConverter(typeof(SafeEnumConverter))]
        public ChargeFlowActionType NextAction;

        [JsonProperty("redirect")]
        public ChargeFlowRedirect Redirect;

        [JsonProperty("returnUrl")]
        public String ReturnUrl;
    }
    
    public class ChargeFlowRedirect : BaseResponse
    {
        [JsonProperty("redirectUrl")]
        public String redirectUrl;
    }
}


