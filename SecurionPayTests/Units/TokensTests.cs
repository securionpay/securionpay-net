using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurionPay.Request;
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
            var requestTester = new RequestTester(PrivateKey, GatewayAdress);
            var tokenRequest  = new TokenRequest() { Number = "4012000100000007", ExpMonth = "11", ExpYear = "2016", Cvc = "432", CardholderName = "Jan Kowalski" };
            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.CreateToken(tokenRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Address = GatewayAdress+"/tokens",
                    Header = GetDesiredHeader(),
                    Content = ToJson(tokenRequest)
                }
            );
        }

        [TestMethod]
        public async Task RetrieveTokenTest()
        {
            var requestTester = new RequestTester(PrivateKey, GatewayAdress);
            var tokenId = "1";
            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.RetrieveToken(tokenId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Address = string.Format("{0}/tokens/{1}", GatewayAdress, tokenId),
                    Header = GetDesiredHeader(),
                    Content = null
                }
            );
        }


        //retrieve

    }
}
