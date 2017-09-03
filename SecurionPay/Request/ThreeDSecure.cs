using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Request
{
    public class ThreeDSecure
    {
        [JsonProperty("requireAttempt")]
        public bool RequireAttempt { get; set; }

        [JsonProperty("requireEnrolledCard")]
        public bool RequireEnrolledCard { get; set; }

        [JsonProperty("requireSuccessfulLiabilityShiftForEnrolledCard")]
        public bool RequireSuccessfulLiabilityShiftForEnrolledCard { get; set; }
    }
}
