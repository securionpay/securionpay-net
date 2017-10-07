using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SecurionPay.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Response
{
    public class Customer : BaseResponse
    {
        [JsonProperty("id")]
        public String Id { get; set; }

        [JsonProperty("created")]
        [JsonConverter(typeof(DateConverter))]
        public DateTime Created { get; set; }

        [JsonProperty("Email")]
        public String Email { get; set; }

        [JsonProperty("description")]
        public String Description { get; set; }

        [JsonProperty("defaultCardId")]
        public String DefaultCardId { get; set; }

        public Card GetDefaultCard()
        {
            return Cards.FirstOrDefault(x => x.Id == DefaultCardId);
        }

        [JsonProperty("cards")]
        public List<Card> Cards { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<String, String> Metadata { get; set; }
    }
}