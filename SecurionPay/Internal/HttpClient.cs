using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Internal
{
    public class HttpClient : IHttpClient
    {
        private System.Net.Http.HttpClient _client=new System.Net.Http.HttpClient();

        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> SendAsync(System.Net.Http.HttpRequestMessage request)
        {
            return await _client.SendAsync(request);
        }

        public void SetAuthorizationHeader(System.Net.Http.Headers.AuthenticationHeaderValue headerValue)
        {
            _client.DefaultRequestHeaders.Authorization = headerValue;
        }

        public void AddHeader(string name, string value)
        {
            _client.DefaultRequestHeaders.Add(name, value);
        }
    }
}
