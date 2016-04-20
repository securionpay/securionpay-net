using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurionPay;
using SecurionPay.Exception;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.Integration
{
    public class IntergationTest
    {
        protected SecurionPayGateway _gateway;
        protected Random _random;

        public IntergationTest()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var gatewayUrl=config.AppSettings.Settings["gateway_test_url"].Value;
            var privateKey= config.AppSettings.Settings["gateway_test_key"].Value;
            _gateway = new SecurionPayGateway(privateKey,gatewayUrl );
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
        
    }
}
