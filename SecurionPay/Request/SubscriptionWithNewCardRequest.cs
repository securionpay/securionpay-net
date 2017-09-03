using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Request
{
    public class SubscriptionWithNewCardRequest: SubscriptionRequest
    {
        [JsonProperty("card")]
        public CardRequest Card { get; set; }
    }
}
