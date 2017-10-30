using Newtonsoft.Json;
using SecurionPay.Converters;
using SecurionPay.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Response
{
    public class FileUpload
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        [JsonConverter(typeof(DateConverter))]
        public DateTime Created { get; set; }

        [JsonProperty("purpose")]
        [JsonConverter(typeof(SafeEnumConverter))]
        public FileUploadPurpose Purpose { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(SafeEnumConverter))]
        public FileUploadType Type { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
