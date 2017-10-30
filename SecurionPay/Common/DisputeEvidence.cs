using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Common
{
    public class DisputeEvidence
    {
        [JsonProperty("productDescription")]
        public string ProductDescription { get; set; }

        [JsonProperty("customerEmail")]
        public string CustomerEmail { get; set; }

        [JsonProperty("customerPurchaseIp")]
        public string CustomerPurchaseIp { get; set; }

        [JsonProperty("customerSignature")]
        public string CustomerSignature { get; set; }

        [JsonProperty("billingAddress")]
        public string BillingAddress { get; set; }

        [JsonProperty("receipt")]
        public string Receipt { get; set; }

        [JsonProperty("customerCommunication")]
        public string CustomerCommunication { get; set; }

        [JsonProperty("serviceDate")]
        public string ServiceDate { get; set; }

        [JsonProperty("serviceDocumentation")]
        public string ServiceDocumentation { get; set; }

        [JsonProperty("duplicateChargeId")]
        public string DuplicateChargeId { get; set; }

        [JsonProperty("duplicateChargeDocumentation")]
        public string DuplicateChargeDocumentation { get; set; }

        [JsonProperty("duplicateChargeExplanation")]
        public string DuplicateChargeExplanation { get; set; }

        [JsonProperty("refundPolicy")]
        public string RefundPolicy { get; set; }

        [JsonProperty("refundPolicyDisclosure")]
        public string RefundPolicyDisclosure { get; set; }

        [JsonProperty("refundRefusalExplanation")]
        public string RefundRefusalExplanation { get; set; }

        [JsonProperty("cancellationPolicy")]
        public string CancellationPolicy { get; set; }

        [JsonProperty("cancellationPolicyDisclosure")]
        public string CancellationPolicyDisclosure { get; set; }

        [JsonProperty("cancellationRefusalExplanation")]
        public string CancellationRefusalExplanation { get; set; }

        [JsonProperty("accessActivityLogs")]
        public string AccessActivityLogs { get; set; }

        [JsonProperty("shippingAddress")]
        public string ShippingAddress { get; set; }

        [JsonProperty("shippingDate")]
        public string ShippingDate { get; set; }

        [JsonProperty("shippingCarrier")]
        public string ShippingCarrier { get; set; }

        [JsonProperty("shippingTrackingNumber")]
        public string ShippingTrackingNumber { get; set; }

        [JsonProperty("shippingDocumentation")]
        public string ShippingDocumentation { get; set; }

        [JsonProperty("uncategorizedText")]
        public string UncategorizedText { get; set; }

        [JsonProperty("uncategorizedFile")]
        public string UncategorizedFile { get; set; }
    }
}
