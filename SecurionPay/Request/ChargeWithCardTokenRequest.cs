using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Request
{
    public class ChargeWithCardTokenRequest : ChargeRequest
    {
        [JsonProperty("card")]
        public string CardToken{ get; set; }
    }
}
