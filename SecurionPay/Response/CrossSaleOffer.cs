using Newtonsoft.Json;
using SecurionPay.Converters;
using SecurionPay.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Response
{
    public class CrossSaleOffer
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        [JsonConverter(typeof(DateConverter))]
        public DateTime Created { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("charge")]
        public CrossSaleOfferCharge Charge { get; set; }

        [JsonProperty("subscription")]
        public CrossSaleOfferSubscription Subscription{ get; set; }

        [JsonProperty("template")]
        [JsonConverter(typeof(SafeEnumConverter))]
        public CrossSaleOfferTemplate Template { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        [JsonProperty("companyLocation")]
        public string CompanyLocation { get; set; }

        [JsonProperty("termsAndConditionsUrl")]
        public string TermsAndConditionsUrl { get; set; }

        [JsonProperty("partnerId")]
        public string PartnerId { get; set; }

        [JsonProperty("visibleForAllPartners")]
        public bool VisibleForAllPartners { get; set; }

        [JsonProperty("visibleForPartnerIds")]
        public List<string> VisibleForPartnerIds { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<String, String> Metadata { get; set; }

    }
}
