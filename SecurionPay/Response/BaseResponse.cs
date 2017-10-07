using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Response
{
    public class BaseResponse
    {
        [JsonExtensionData]
        public IDictionary<string, JToken> Other;
    }
}
