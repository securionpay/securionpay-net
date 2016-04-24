using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.Units
{
    public class BaseUnitTestsSet
    {
        private string _appVersion;
        private string _gatewayAdress;
        private string _privateKey = "test";

        public string GatewayAdress { get { return _gatewayAdress; } }
        public string PrivateKey { get { return _privateKey; } }

        [TestInitialize]
        public void InitializeTests()
        {
            _gatewayAdress = "https://api.securionpay.com";
            var assemblyVersion = Assembly.Load("SecurionPay").GetName().Version;
            _appVersion = string.Format("{0}.{1}.{2}", assemblyVersion.Major, assemblyVersion.MajorRevision, assemblyVersion.Minor);
        }

        protected List<string> GetDesiredHeader()
        {
            return new List<string>() { string.Format("Authorization: Basic {0}", Base64Encode(_privateKey + ":")), string.Format("User-Agent: SecurionPay-DOTNET/{0}", _appVersion) };
        }

        protected string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }

        protected string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
