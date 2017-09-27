using Newtonsoft.Json;
using SecurionPay.Converters;
using SecurionPay.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SecurionPay.Request
{
    public class FileUploadRequest
    {
        [JsonProperty("purpose")]
        [JsonConverter(typeof(SafeEnumConverter))]
        public FileUploadPurpose Purpose { get; set; }

        public byte[] File { get; set; }
    }
}
