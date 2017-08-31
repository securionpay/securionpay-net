using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Internal
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private string _secretKey;
        private string _apiUrl;

        public ConfigurationProvider(string secretKey, string apiUrl)
        {
            _secretKey = secretKey;
            _apiUrl = apiUrl;
        }

        public string GetSecretKey()
        {
            return _secretKey;
        }

        public string GetApiUrl()
        {
            return _apiUrl;
        }
    }
}
