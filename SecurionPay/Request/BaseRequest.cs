using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Request
{
    public class BaseRequest
    {
        [JsonExtensionData]
        public IDictionary<string, JToken> Other;
    }
}
