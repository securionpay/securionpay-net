using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SecurionPay.Converters
{
    public class SafeEnumConverter : StringEnumConverter
    {
        private string fallback = "Unrecognized";
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var rawValue=(string)reader.Value;
            var enumValues = objectType.GetMembers();
            var attributes = enumValues.Select(x => x.GetCustomAttributes(typeof(EnumMemberAttribute),true)).SelectMany(y=>y).Select(z=>((EnumMemberAttribute)z).Value);
            if (Enum.GetNames(objectType).Contains(rawValue) || attributes.Contains(rawValue))
            {
                return base.ReadJson(reader, objectType, existingValue, serializer);
            }
            else
            {
                return Enum.Parse(objectType, fallback,true);
            }
        }
    }
}
