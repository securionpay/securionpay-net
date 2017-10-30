using SecurionPay;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SecurionPayTests.Integration
{
    public class TestConfigurationProvider : ISecretKeyProvider, IConfigurationProvider
    {
        Dictionary<string, string> _config;
        public TestConfigurationProvider()
        {
            var xml = XDocument.Load("App.config");
            _config = xml.Descendants().Where(des => des.Name == "appSettings").First().Descendants().ToDictionary(xelem => xelem.Attribute("key").Value, xelem => xelem.Attribute("value").Value);
        }

        public string GetApiUrl()
        {
            return _config["gateway_test_url"];
        }

        public string GetUploadsUrl()
        {
            return _config["uploads_test_url"];
        }

        public string GetSecretKey()
        {
            return _config["gateway_test_key"];
        }
    }
}
