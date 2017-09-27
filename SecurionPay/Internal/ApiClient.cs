using Newtonsoft.Json;
using SecurionPay.Exception;
using SecurionPay.Internal;
using SecurionPay.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPay
{
    public class ApiClient : IApiClient
    {
        private string _privateAuthToken;
        private string _version = "2.3.0";
        private HttpClient client;

        public ApiClient(ISecretKeyProvider secretKeyProvider)
            : this(secretKeyProvider.GetSecretKey())
        {
        }

        public ApiClient(string secretKey, HttpMessageHandler customHttpMessageHandler = null)
        {
            var tokenBytes = Encoding.UTF8.GetBytes(secretKey + ":");
            _privateAuthToken = Convert.ToBase64String(tokenBytes);
            if (customHttpMessageHandler == null)
            {
                client = new HttpClient();
            }
            else
            {
                client = new HttpClient(customHttpMessageHandler);
            }
        }

        public async Task<T> SendRequest<T>(HttpMethod method, string url, object parameter)
        {

            HttpRequestMessage request = new HttpRequestMessage(method,url);
            if (parameter != null)
            {
                var requestJson = JsonConvert.SerializeObject(parameter, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            }

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", _privateAuthToken);
            client.DefaultRequestHeaders.Add("User-Agent", string.Format("SecurionPay-DOTNET/{0}", _version));
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiResponseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(apiResponseString);
            }
            else
            {
                ErrorResponse errorResponse;
                var apiErrorRsponseString = await response.Content.ReadAsStringAsync();
                errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(apiErrorRsponseString);
                throw new SecurionPayException(errorResponse.Error, typeof(T).Name, url);
            }
        }

        public async Task<T> SendMultiPartRequest<T>(HttpMethod method, string url,Dictionary<string,string> form,byte[] fileBody,string fileName)
        {
            HttpRequestMessage request = new HttpRequestMessage(method, url);

            var content = new MultipartFormDataContent();

            var fileContent = new ByteArrayContent(fileBody);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName
            };
            content.Add(fileContent);

            foreach (var formEntry in form)
            {
                content.Add(new StringContent(formEntry.Value), formEntry.Key);
            }

            request.Content = content;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _privateAuthToken);
            client.DefaultRequestHeaders.Add("User-Agent", string.Format("SecurionPay-DOTNET/{0}", _version));
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiResponseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(apiResponseString);
            }
            else
            {
                ErrorResponse errorResponse;
                var apiErrorRsponseString = await response.Content.ReadAsStringAsync();
                errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(apiErrorRsponseString);
                throw new SecurionPayException(errorResponse.Error, typeof(T).Name, url);
            }
        }
    }
}
