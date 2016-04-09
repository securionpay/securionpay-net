using SecurionPay;
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
        public IntergationTest()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var gatewayUrl=config.AppSettings.Settings["gateway_test_url"].Value;
            var privateKey= config.AppSettings.Settings["gateway_test_key"].Value;
            _gateway = new SecurionPayGateway(privateKey,gatewayUrl );

        }
        
    }
}
