using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SecurionPay.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace SecurionPay.Converters
{
    public class SafeEnumConverter : StringEnumConverter
    {
        private string fallback = "Unrecognized";
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (!objectType.IsEnumeration())
            {
                objectType = Nullable.GetUnderlyingType(objectType);
                if (objectType==null)
                {
                    throw new ArgumentException("SafeEnumConverter attribute can be only used for Enum and Enum? types");
                }
            }
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
