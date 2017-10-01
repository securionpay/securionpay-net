using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SecurionPay.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Converters
{
    public class DateConverter : StringEnumConverter
    {
        UnixDateConverter _unixDateConverter = new UnixDateConverter();
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var rawSeconds = (long)reader.Value;
            return _unixDateConverter.FromUnixTimeStamp(rawSeconds).ToLocalTime();
        }
    }
}
