using Xunit;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using SecurionPay;
using SecurionPay.Enums;
using SecurionPay.Request;
using SecurionPay.Response;
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
        public class BlackListTests :BaseUnitTestsSet
    {

        [Fact]
        public async Task CreateFingerprintBlackListTest()
        {
            await CreatelBlackListTest(new BlacklistRuleRequest() { RuleType = BlacklistRuleType.Fingerprint, Fingerprint = "test_fp" + DateTime.Now.Millisecond });
        }

        [Fact]
        public async Task CreateEmailBlackListTest()
        {
            await CreatelBlackListTest(new BlacklistRuleRequest() { RuleType = BlacklistRuleType.Email, Email = "test" + DateTime.Now.Millisecond + "@example.com" });
        }

        [Fact]
        public async Task CreateLanguageBlackListTest()
        {
            await CreatelBlackListTest(new BlacklistRuleRequest() { RuleType = BlacklistRuleType.AcceptLanguage, AcceptLanguage = "test" + DateTime.Now.Millisecond});
        }

        [Fact]
        public async Task CreateIpBlackListTest()
        {
            await CreatelBlackListTest(new BlacklistRuleRequest() { RuleType = BlacklistRuleType.IpAddress, IpAddress = "192.168.11.1" });
        }

        [Fact]
        public async Task CreateIpCountryBlackListTest()
        {
            await CreatelBlackListTest(new BlacklistRuleRequest() { RuleType = BlacklistRuleType.IpCountry, IpCountry = "PL" });
        }

        [Fact]
        public async Task CreateMetadataBlackListTest()
        {
            await CreatelBlackListTest(new BlacklistRuleRequest() { RuleType = BlacklistRuleType.Metadata, MetadataKey = "key",MetadataValue="value" });
        }

        [Fact]
        public async Task CreateUserAgentBlackListTest()
        {
            await CreatelBlackListTest(new BlacklistRuleRequest() { RuleType = BlacklistRuleType.UserAgent, IpCountry = "UA" });
        }


        [Fact]
        public async Task RetrieveBlackListTest()
        {
            var requestTester = GetRequestTester();
            var ruleId = "test" + DateTime.Now.Millisecond;
            await requestTester.TestMethod<BlacklistRule>(
                async (api) =>
                {
                    await api.RetrieveBlacklistRule(ruleId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Action =  "blacklist/" + ruleId,
                    Parameter = null
                }
            );
        }

        [Fact]
        public async Task DeleteBlackListTest()
        {
            var requestTester = GetRequestTester();
            var ruleId = "test" + DateTime.Now.Millisecond;
            await requestTester.TestMethod<DeleteResponse>(
                async (api) =>
                {
                    await api.DeleteBlacklistRule(ruleId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Delete,
                    Action = "blacklist/" + ruleId,
                    Parameter = null
                }
            );
        }

        [Fact]
        public async Task ListBlackListTest()
        {
            var requestTester = GetRequestTester();
            await requestTester.TestMethod<SecurionpayList>(
                async (api) =>
                {
                    await api.ListBlacklistRules();
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Action = "blacklist",
                    Parameter = null
                }
            );
        }

        #region private

        private async Task CreatelBlackListTest(BlacklistRuleRequest createRequest)
        {
            var requestTester = GetRequestTester();
            await requestTester.TestMethod<BlacklistRule>(
                async (api) =>
                {
                    var rule = await api.CreateBlacklistRule(createRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Action = "blacklist",
                    Parameter = createRequest
                }
            );
        }

        #endregion
    }
}
