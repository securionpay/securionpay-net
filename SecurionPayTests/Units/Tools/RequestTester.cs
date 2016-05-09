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
        string _secretKey;
        SemaphoreSlim semaphore = new SemaphoreSlim(0);
        public RequestTester(string secretKey,string gatewayAddress)
        {
            _gatewayAddress = gatewayAddress;
            _secretKey = secretKey;
        }

        public async Task TestMethod(Func<SecurionPayGateway, Task> methodToTest, RequestDescriptor expectedRequest)
        {
            var mock = new Mock<HttpMessageHandler>();
            HttpRequestMessage request=null;
            string requestJson = null;

            mock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Callback< HttpRequestMessage,CancellationToken>(async (htm,ct)=>
                {
                    try
                    {
                        request = htm;
                        if (htm.Content != null)
                        {
                            requestJson = await htm.Content.ReadAsStringAsync();
                        }
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                })
                .Returns(Task.Run(() => new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadGateway }));
            SecurionPayGateway api = new SecurionPayGateway(_secretKey, _gatewayAddress, mock.Object);
            
            try
            {
                await methodToTest(api);
            }
            catch { }
            await semaphore.WaitAsync();
            var result=expectedRequest.Match(request, requestJson);
            Assert.IsTrue(result.MatchSuccess,result.MatchFailReasonsMessage);
        }
    }
}
