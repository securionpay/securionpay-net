using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SecurionPay.Common;
using SecurionPay.Enums;
using SecurionPay.Converters;

namespace SecurionPay.Request
{
    public class PaymentMethodRequest : BaseRequest
    {

        [JsonProperty("id")]
        public String Id { get; set; }

        [JsonProperty("customerId")]
        public String CustomerId { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(SafeEnumConverter))]
        public PaymentMethodType Type { get; set; }

        [JsonProperty("billing")]
        public Billing Billing { get; set; }
    }
}
