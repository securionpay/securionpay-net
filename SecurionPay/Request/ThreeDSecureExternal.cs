using Newtonsoft.Json;
using SecurionPay.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Request
{
    public class ThreeDSecureExternal
    {

        [JsonProperty("version")]
		public String Version { get; set; }

        [JsonProperty("xid")]
		public String Xid { get; set; }

        [JsonProperty("eci")]
		public String Eci { get; set; }

        [JsonProperty("authenticationValue")]
		public String AuthenticationValue { get; set; }

        [JsonProperty("dsTransactionId")]
		public String DsTransactionId { get; set; }

        [JsonProperty("acsTransactionId")]
		public String AcsTransactionId { get; set; }

        [JsonProperty("status")]
		public ThreeDSecureExternalStatus Status { get; set; }
    }
}
