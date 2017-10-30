using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurionPay.Internal
{
    public class ConfigurationProvider : IConfigurationProvider, ISecretKeyProvider
    {
        private string _secretKey;
        private string _apiUrl;
        private string _uploadsUrl;

        public ConfigurationProvider(string secretKey, string apiUrl, string uploadsUrl)
        {
            _secretKey = secretKey;
            _apiUrl = apiUrl;
            _uploadsUrl = uploadsUrl;
        }

        public string GetSecretKey()
        {
            return _secretKey;
        }

        public string GetApiUrl()
        {
            return _apiUrl;
        }

        public string GetUploadsUrl()
        {
            return _uploadsUrl;
        }
    }
}
