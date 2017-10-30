using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SecurionPay.Enums
{
    public enum FileUploadPurpose
    {
        [EnumMember(Value = "dispute_evidence")]
        DisputeEvidence,

        //Used when received value can't be mapped to this enumeration.
        Unrecognized
    }
}
