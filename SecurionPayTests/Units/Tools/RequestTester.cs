using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using SecurionPay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecurionPayTests.Units.Tools
{
    public class RequestTester
    {
        string _gatewayAddress;
        string _privateKey;

        public RequestTester(string privateKey,string gatewayAddress)
        {
            _gatewayAddress = gatewayAddress;
            _privateKey = privateKey;
        }

        public async Task TestMethod(Func<SecurionPayGateway, Task> methodToTest, RequestDescriptor expectedRequest)
        {
            var mock = new Mock<HttpMessageHandler>();
            HttpRequestMessage request=null;
            string requestJson = null;
            mock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Callback< HttpRequestMessage,CancellationToken>(async (htm,ct)=>
                {
                    requestJson = await htm.Content.ReadAsStringAsync();
                    request = htm;
                })
                .Returns(Task.Run(() => new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadGateway }));
            SecurionPayGateway api = new SecurionPayGateway(_privateKey, _gatewayAddress, mock.Object);
            
            try
            {
                await methodToTest(api);
            }
            catch { }
            Assert.IsTrue(expectedRequest.Match(request, requestJson));
        }
    }
}
