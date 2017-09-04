using Newtonsoft.Json;
using SecurionPay.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Converters
{
    public class ChargeCardDefinitionSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if(value is ChargeCardTokenDefinition)
            {
                serializer.Serialize(writer, ((ChargeCardTokenDefinition)value).CardToken);
            }
            if (value is ChargeExistingCardDefinition)
            {
                serializer.Serialize(writer, ((ChargeExistingCardDefinition)value).CardId);
            }
            if (value is ChargeNewCardDefinition)
            {
                serializer.Serialize(writer, ((ChargeNewCardDefinition)value).NewCardRequest);
            }
            
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
