using Newtonsoft.Json;
using SecurionPay.Converters;
using SecurionPay.Enums;
using System;

namespace SecurionPay.Response
{
    public class Event
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(SafeEnumConverter))]
        public EventType Type { get; set; }

        [JsonProperty("data")]
        [JsonConverter(typeof(EventDataConverter))]
        public object Data { get; set; }

        [JsonProperty("log")]
        public string Log { get; set; }

        [JsonProperty("created")]
        [JsonConverter(typeof(DateConverter))]
        public DateTime Created { get; set; }
    }
}