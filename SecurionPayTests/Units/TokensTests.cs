using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurionPay.Request;
using SecurionPay.Response;
using SecurionPayTests.Units.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.Units
{
    [TestClass]
    public class TokensTests :BaseUnitTestsSet
    {
        [TestMethod]
        public async Task CreateTokenTest()
        {
            var requestTester = GetRequestTester();
            var tokenRequest  = new TokenRequest() { Number = "4012000100000007", ExpMonth = "11", ExpYear = "2016", Cvc = "432", CardholderName = "John Smith" };
            await requestTester.TestMethod<Token>(
                async (api) =>
                {
                    await api.CreateToken(tokenRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Action = "tokens",
                    Parameter = tokenRequest
                }
            );
        }

        [TestMethod]
        public async Task RetrieveTokenTest()
        {
            var requestTester = GetRequestTester();
            var tokenId = "1";
            await requestTester.TestMethod<Token>(
                async (api) =>
                {
                    await api.RetrieveToken(tokenId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Action = string.Format("tokens/{0}", tokenId),
                    Parameter = null
                }
            );
        }


        //retrieve

    }
}
