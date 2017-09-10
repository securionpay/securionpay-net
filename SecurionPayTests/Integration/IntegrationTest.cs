using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurionPay;
using SecurionPay.Exception;
using SecurionPay.Internal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.Integration
{
    public class IntegrationTest
    {
        protected SecurionPayGateway _gateway;
        protected Random _random;

        public IntegrationTest()
        {
            var configProvider = new TestConfigurationProvider();
            var apiClient = new ApiClient(configProvider);
            var signService = new SignService(configProvider);
            _gateway = new SecurionPayGateway(apiClient, configProvider, signService);
            _random = new Random();
        }

        protected void HandleApiException(SecurionPayException exc)
        {
            Assert.Fail(string.Format("SecurionPayException  was thrown with message: {0},code:{1},request type:{2},action:{3}",
                          exc.Error.Message,
                          exc.Error.Code.HasValue ? exc.Error.Code.Value.ToString() : "no code",
                          exc.RequestType,
                          exc.RequestAction)

            );
        }

        protected string GetRandomEmail()
        {
            return string.Format("test{0}@test.com", _random.Next(999999));
        }

        protected string CorrectCardExpiryYear
        {
            get
            {
                return (DateTime.Today.Year + 1).ToString();
            }
        }

    }
}
