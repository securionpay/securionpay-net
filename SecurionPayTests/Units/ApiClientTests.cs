using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using SecurionPay;
using SecurionPay.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecurionPayTests.Units
{
    [TestClass]
    public class ApiClientTests
    {
        [TestMethod]
        public async Task ApiClientBuildsRequestWithCorrectContentTest()
        {
            SemaphoreSlim semaphore = new SemaphoreSlim(0);
            var httpclientMock = new Mock<IHttpClient>();
            string requestJson=null;
            httpclientMock.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>())).Callback<HttpRequestMessage>(async (httpRequestMessage) =>
                {
                    try
                    {
                        if (httpRequestMessage.Content != null)
                        {
                            requestJson = await httpRequestMessage.Content.ReadAsStringAsync();
                        }
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }).Returns(Task.Run(() => new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.OK,Content = new ByteArrayContent(new byte[1]) }));

            var mapperMock = new Mock<IFileExtensionToMimeMapper>();
            var keyProviderMock = new Mock<ISecretKeyProvider>();
            var apiClient = new ApiClient(httpclientMock.Object, keyProviderMock.Object, mapperMock.Object);

            await apiClient.SendRequest<TestRequest>(HttpMethod.Put, "https://testAction.com", new TestParameter() { TestValue="t1"});
            await semaphore.WaitAsync();

            Assert.AreEqual(requestJson, "{\"TestValue\":\"t1\"}");

        }

        [TestMethod]
        public async Task ApiClientBuildsRequestWithCorrectUrlAndMethodTest()
        {

            var httpclientMock = new Mock<IHttpClient>();
            httpclientMock.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>())).Returns(Task.Run(() => new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.OK, Content = new ByteArrayContent(new byte[1]) }));

            var mapperMock = new Mock<IFileExtensionToMimeMapper>();
            var keyProviderMock = new Mock<ISecretKeyProvider>();
            var apiClient = new ApiClient(httpclientMock.Object, keyProviderMock.Object, mapperMock.Object);

            await apiClient.SendRequest<TestRequest>(HttpMethod.Put, "https://testAction.com", new TestParameter());


            httpclientMock.Verify(client => client.SendAsync(It.Is<HttpRequestMessage>(message => message.Method == HttpMethod.Put &&
                                                                                                  message.RequestUri == new Uri("https://testAction.com"))));
        }

        [TestMethod]
        public async Task ApiClientBuildsRequestWithCorrectHeadersTest()
        {
            var assemblyVersion = Assembly.Load("SecurionPay").GetName().Version;
            var appVersion = string.Format("{0}.{1}.{2}", assemblyVersion.Major, assemblyVersion.Minor, assemblyVersion.Build);

            var httpclientMock = new Mock<IHttpClient>();
            httpclientMock.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>())).Returns(Task.Run(() => new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.OK, Content = new ByteArrayContent(new byte[1]) }));

            var mapperMock = new Mock<IFileExtensionToMimeMapper>();
            var keyProviderMock = new Mock<ISecretKeyProvider>();
            keyProviderMock.Setup(provider => provider.GetSecretKey()).Returns("key");
            var apiClient = new ApiClient(httpclientMock.Object, keyProviderMock.Object, mapperMock.Object);

            await apiClient.SendRequest<TestRequest>(HttpMethod.Put, "https://testAction.com", new TestParameter());

            httpclientMock.Verify(client => client.SetAuthorizationHeader(It.Is<AuthenticationHeaderValue>(header => header.Scheme == "Basic" && header.Parameter == "a2V5Og==")));
            httpclientMock.Verify(client => client.AddHeader(It.Is<string>(name => name == "User-Agent"), It.Is<string>(value => value == string.Format("SecurionPay-DOTNET/{0}", appVersion))));

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
