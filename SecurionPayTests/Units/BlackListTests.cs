using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using SecurionPay;
using SecurionPay.Enums;
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
        string _gatewayAdress;
        string _privateKey="test";
        string _appVersion;

        [TestInitialize]
        public void InitializeTests()
        {
            _gatewayAdress = "https://api.securionpay.com";
            var assemblyVersion = Assembly.Load("SecurionPay").GetName().Version;
            _appVersion = string.Format("{0}.{1}.{2}", assemblyVersion.Major, assemblyVersion.MajorRevision, assemblyVersion.Minor);
        }

        [TestMethod]
        public async Task CreateFingerprintBlackListTest()
        {
            await CreatelBlackListTest(new BlacklistRuleRequest() { RuleType = BlacklistRuleType.Fingerprint, Fingerprint = "test_fp" + DateTime.Now.Millisecond });
        }

        [TestMethod]
        public async Task CreateEmailBlackListTest()
        {
            await CreatelBlackListTest(new BlacklistRuleRequest() { RuleType = BlacklistRuleType.Email, Email = "test" + DateTime.Now.Millisecond + "@example.com" });
        }

        [TestMethod]
        public async Task CreateLanguageBlackListTest()
        {
            await CreatelBlackListTest(new BlacklistRuleRequest() { RuleType = BlacklistRuleType.AcceptLanguage, AcceptLanguage = "test" + DateTime.Now.Millisecond});
        }

        [TestMethod]
        public async Task CreateIpBlackListTest()
        {
            await CreatelBlackListTest(new BlacklistRuleRequest() { RuleType = BlacklistRuleType.IpAddress, IpAddress = "192.168.11.1" });
        }

        [TestMethod]
        public async Task CreateIpCountryBlackListTest()
        {
            await CreatelBlackListTest(new BlacklistRuleRequest() { RuleType = BlacklistRuleType.IpCountry, IpCountry = "PL" });
        }

        [TestMethod]
        public async Task CreateMetadataBlackListTest()
        {
            await CreatelBlackListTest(new BlacklistRuleRequest() { RuleType = BlacklistRuleType.Metadata, MetadataKey = "key",MetadataValue="value" });
        }

        [TestMethod]
        public async Task CreateUserAgentBlackListTest()
        {
            await CreatelBlackListTest(new BlacklistRuleRequest() { RuleType = BlacklistRuleType.UserAgent, IpCountry = "UA" });
        }


        [TestMethod]
        public async Task RetrieveBlackListTest()
        {
            var requestTester = new RequestTester(_privateKey, _gatewayAdress);
            var ruleId = "test" + DateTime.Now.Millisecond;
            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.RetrieveBlacklistRule(ruleId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Address = _gatewayAdress + "/blacklist/" + ruleId,
                    Header = GetDesiredHeader(),
                    Content = null
                }
            );
        }


        #region private

        private async Task CreatelBlackListTest(BlacklistRuleRequest createRequest)
        {
            var requestTester = new RequestTester(_privateKey, _gatewayAdress);
            await requestTester.TestMethod(
                async (api) =>
                {
                    var rule = await api.CreateBlacklistRule(createRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Address = _gatewayAdress + "/blacklist",
                    Header = GetDesiredHeader(),
                    Content = ToJson(createRequest)
                }
            );
        }

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
