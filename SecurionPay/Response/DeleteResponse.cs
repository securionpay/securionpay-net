using Newtonsoft.Json;

namespace SecurionPay.Response
{
    public class DeleteResponse
    {

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}