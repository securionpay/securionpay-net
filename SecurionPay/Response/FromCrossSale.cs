using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Response
{
    public class FromCrossSale : BaseResponse
    {
        [JsonProperty("offerId")]
        public string OfferId { get; set; }

        [JsonProperty("partnerId")]
        public string PartnerId { get; set; }

    }
}
