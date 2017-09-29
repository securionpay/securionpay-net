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
        private IFileExtensionToMimeMapper _fileExtensionToMimeMapper;

        public ApiClient(ISecretKeyProvider secretKeyProvider, IFileExtensionToMimeMapper fileExtensionToMimeMapper)
            : this(secretKeyProvider.GetSecretKey(), fileExtensionToMimeMapper)
        {
        }

        public ApiClient(string secretKey,IFileExtensionToMimeMapper fileExtensionToMimeMapper,HttpMessageHandler customHttpMessageHandler = null)
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
            _fileExtensionToMimeMapper = fileExtensionToMimeMapper;
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
            foreach (var formEntry in form)
            {
                var stringContent = new StringContent(formEntry.Value);
                stringContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = string.Format("\"{0}\"",formEntry.Key)
                };
                content.Add(stringContent, formEntry.Key);
            }

            var fileContent = new ByteArrayContent(fileBody);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "\"file\"",
                FileName = string.Format("\"{0}\"",fileName)
            };

            var mimeType = _fileExtensionToMimeMapper.GetMimeType(fileName);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(mimeType);
            content.Add(fileContent);

 
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
