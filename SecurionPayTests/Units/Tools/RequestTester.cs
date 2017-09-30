using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using SecurionPay;
using SecurionPay.Internal;
using SecurionPay.Response;
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

        public RequestTester()
        {

        }

        public async Task TestMethod<TResponseType>(Func<SecurionPayGateway, Task> methodToTest, RequestDescriptor expectedRequest)
        {
            var apiClientMock = new Mock<IApiClient>();
            var signMock = new Mock<ISignService>();
            var configMock = new Mock<IConfigurationProvider>();
            configMock.Setup<string>(x => x.GetApiUrl()).Returns("https://example.com");
            configMock.Setup<string>(x => x.GetUploadsUrl()).Returns("https://example2.com");
            SecurionPayGateway gateway = new SecurionPayGateway(apiClientMock.Object, configMock.Object, signMock.Object);

            try
            {
                await methodToTest(gateway);
            }
            catch { }
            var endpoint = expectedRequest.UseUploadEndpoint ? configMock.Object.GetUploadsUrl()  : configMock.Object.GetApiUrl();
            apiClientMock.Verify<Task<TResponseType>>(api => api.SendRequest<TResponseType>(It.Is<HttpMethod>(method => method == expectedRequest.Method), It.Is<string>(action => action == endpoint +"/"+ expectedRequest.Action), It.Is<object>(obj=>obj==expectedRequest.Parameter)), Times.Once);
        }
    }
}
