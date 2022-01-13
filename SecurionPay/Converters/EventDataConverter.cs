using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SecurionPay.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Converters
{
    public class EventDataConverter : JsonConverter
    {

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            switch (jObject.GetValue("objectType").ToString())
            {
                case "charge":
                    var charge = new Charge();
                    serializer.Populate(jObject.CreateReader(), charge);
                    return charge;
                case "credit":
                    var credit = new Credit();
                    serializer.Populate(jObject.CreateReader(), credit);
                    return credit;
                case "dispute":
                    var dispute = new Dispute();
                    serializer.Populate(jObject.CreateReader(), dispute);
                    return dispute;
                case "subscription":
                    var subscription = new Subscription();
                    serializer.Populate(jObject.CreateReader(), subscription);
                    return subscription;
                case "plan":
                    var plan = new Plan();
                    serializer.Populate(jObject.CreateReader(), plan);
                    return plan;
                case "customer":
                    var customer = new Customer();
                    serializer.Populate(jObject.CreateReader(), customer);
                    return customer;
                case "fraud_warning":
                    var warning = new FraudWarning();
                    serializer.Populate(jObject.CreateReader(), warning);
                    return warning;
                case "card":
                    var card = new Card();
                    serializer.Populate(jObject.CreateReader(), card);
                    return card;
                case "crossSaleOffer":
                    var crossSaleOffer = new CrossSaleOffer();
                    serializer.Populate(jObject.CreateReader(), crossSaleOffer);
                    return crossSaleOffer;


            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

        }
    }
}
