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
        public TestConfigurationProvider() {
            DotNetEnv.Env.TraversePath().Load();
        }

        public string GetApiUrl()
        {
            return DotNetEnv.Env.GetString("API_URL", "https://api.securionpay.com/");
        }

        public string GetUploadsUrl()
        {
            return DotNetEnv.Env.GetString("UPLOADS_URL", "https://uploads.securionpay.com/");
    
        }

        public string GetSecretKey()
        {
            return DotNetEnv.Env.GetString("SECRET_KEY");
        }
    }
}
