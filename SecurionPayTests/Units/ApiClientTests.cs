using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using SecurionPay;
using SecurionPay.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecurionPayTests.Units
{
    [TestClass]
    public class ApiClientTests
    {

        //todo one test per feature
        [TestMethod]
        public async Task ApiClientBuildsCorrectRequestTest()
        {
            SemaphoreSlim semaphore = new SemaphoreSlim(0);
            var assemblyVersion = Assembly.Load("SecurionPay").GetName().Version;
            var appVersion = string.Format("{0}.{1}.{2}", assemblyVersion.Major, assemblyVersion.Minor, assemblyVersion.Build);

            var mock = new Mock<HttpMessageHandler>();
            HttpRequestMessage request=null;
            string requestJson = null;
            mock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>(async (httpRequestMessage, ct) =>
                {
                    try
                    {
                        request = httpRequestMessage;
                        if (httpRequestMessage.Content != null)
                        {
                            requestJson = await httpRequestMessage.Content.ReadAsStringAsync();
                        }
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                })
                .Returns(Task.Run(() => new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadGateway }));
            var mapperMock = new Mock<IFileExtensionToMimeMapper>();
            var apiClient = new ApiClient("secret",mapperMock.Object, mock.Object);

            try
            {
                await apiClient.SendRequest<TestRequest>(HttpMethod.Put, "https://testAction.com", new TestParameter() { TestValue="t1"});
            }
            catch { }
            await semaphore.WaitAsync();
            Assert.AreEqual(request.Method,HttpMethod.Put);
            Assert.AreEqual(request.RequestUri, "https://testAction.com");
            var expectedHeaders = GetDesiredHeaders("secret", appVersion);
            Assert.IsTrue(request.Headers.All(x => expectedHeaders.Contains(x.Key + ": " + x.Value.First()) && expectedHeaders.Count == request.Headers.Count()));
            Assert.AreEqual(requestJson, "{\"TestValue\":\"t1\"}");

        }

        private List<string> GetDesiredHeaders(string secretKey,string version)
        {
            return new List<string>() { string.Format("Authorization: Basic {0}", Base64Encode(secretKey + ":")), string.Format("User-Agent: SecurionPay-DOTNET/{0}", version) };
        }

        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private class TestRequest
        {

        }

        private class TestParameter
        {
            public string TestValue { get; set; }
        }
    }
}
