using Newtonsoft.Json;

namespace SecurionPay.Response
{
    public class DeleteResponse : BaseResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}