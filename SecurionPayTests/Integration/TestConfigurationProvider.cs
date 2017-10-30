using SecurionPay;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.Integration
{
    public class TestConfigurationProvider : ISecretKeyProvider, IConfigurationProvider
    {
        Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        public string GetApiUrl()
        {
            return _config.AppSettings.Settings["gateway_test_url"].Value;
        }

        public string GetUploadsUrl()
        {
            return _config.AppSettings.Settings["uploads_test_url"].Value;
        }

        public string GetSecretKey()
        {
            return _config.AppSettings.Settings["gateway_test_key"].Value;
        }
    }
}
