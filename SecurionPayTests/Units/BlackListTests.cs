using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using SecurionPay;
using SecurionPay.Request;
using SecurionPayTests.Units.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecurionPayTests.Units
{
    [TestClass]
    public class BlackListTests
    {
        RequestTester _requestTester;
        string _gatewayAdress;
        string _privateKey="test";
        string _appVersion;

        [TestInitialize]
        public void InitializeTests()
        {
            _gatewayAdress = "https://api.securionpay.com";
            _requestTester = new RequestTester(_privateKey, _gatewayAdress);
            var assemblyVersion = Assembly.Load("SecurionPay").GetName().Version;
            _appVersion = string.Format("{0}.{1}.{2}", assemblyVersion.Major, assemblyVersion.MajorRevision, assemblyVersion.Minor);
        }

        [TestMethod]
        public async Task CreateBlackListTest()
        {
            var blacklistRuleRequest = new BlacklistRuleRequest() { Fingerprint = "fssdfhda" + DateTime.Now.Millisecond };
            await _requestTester.TestMethod(
                async (api) =>
                {
                    var rule = await api.CreateBlacklistRule(blacklistRuleRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Address = _gatewayAdress + "/blacklist",
                    Header = GetDesiredHeader(),
                    Content = ToJson(blacklistRuleRequest)
                }
            );
        }

        #region private

        private string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }

        private List<string> GetDesiredHeader()
        {
            return new List<string>() { string.Format("Authorization: Basic {0}", Base64Encode(_privateKey+":")), string.Format("User-Agent: SecurionPay-DOTNET/{0}", _appVersion) };
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        #endregion
    }
}
