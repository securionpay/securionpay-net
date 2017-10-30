using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Response
{
    public class CrossSaleOfferSubscription : BaseResponse
    {
        [JsonProperty("planId")]
        public string PlanId { get; set; }

        [JsonProperty("captureCharges")]
        public bool CaptureCharges { get; set; }
    }
}
