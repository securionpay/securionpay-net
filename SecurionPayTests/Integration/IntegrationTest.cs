using Xunit;
using Xunit.Sdk;
using SecurionPay;
using SecurionPay.Exception;
using SecurionPay.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.Integration
{
    public class IntegrationTest
    {
        protected SecurionPayGateway _gateway;
        protected Random _random=new Random();


        public IntegrationTest()
        {
            var configProvider = new TestConfigurationProvider();
            var mimeMapper = new FileExtensionToMimeMapper();
            var httpClient = new HttpClient();
            var apiClient = new ApiClient(httpClient,configProvider, mimeMapper);
            var signService = new SignService(configProvider);
            _gateway = new SecurionPayGateway(apiClient, configProvider, signService);
        }

        protected void HandleApiException(SecurionPayException exc)
        {
            Fail("SecurionPayException  was thrown with message: {0},code:{1},request type:{2},action:{3}",
                          exc.Error.Message,
                          exc.Error.Code.HasValue ? exc.Error.Code.Value.ToString() : "no code",
                          exc.RequestType,
                          exc.RequestAction
            );
        }

        protected void Fail(string message, params object[] args) 
        {
            throw new XunitException(string.Format(message, args));
        }

        protected string CorrectCardExpiryYear
        {
            get
            {
                return (DateTime.Today.Year + 1).ToString();
            }
        }

        protected async Task WaitUntil(Func<Task<bool>> condition, int timeout, int frequency = 100)
        {
            var waitTask = Task.Run(async () =>
            {
                while (!(await condition())) 
                {
                    await Task.Delay(frequency);
                }
            });

            if (waitTask != await Task.WhenAny(waitTask, Task.Delay(timeout))) 
                throw new TimeoutException();
        }

    }
}
