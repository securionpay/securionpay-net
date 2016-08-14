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
    public class CreditTests : BaseUnitTestsSet
    {
        [TestMethod]
        public async Task CreateCreditTest()
        {
            var requestTester = GetRequestTester();
            var customerId = "1";
            var creditRequest = new CreditRequest() { CustomerId = customerId, CardId="1" ,Amount=100,Currency="EUR"};
            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.CreateCredit(creditRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Address = GatewayAdress + "/credits",
                    Header = GetDesiredHeader(),
                    Content = ToJson(creditRequest)
                }
            );
        }

        [TestMethod]
        public async Task CreateCreditWithCartdTest()
        {
            var requestTester = GetRequestTester();
            var customerId = "1";
            var creditRequest = new CreditWithCardRequest()
            {
                CustomerId = customerId,
                Card = new CardRequest() { CustomerId = customerId, Number = "404129331232", ExpMonth = "6", ExpYear = "2015", CardholderName = "John Smith" },
                Amount = 100,
                Currency = "EUR"
            };
            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.CreateCredit(creditRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Address = GatewayAdress + "/credits",
                    Header = GetDesiredHeader(),
                    Content = ToJson(creditRequest)
                }
            );
        }

        [TestMethod]
        public async Task RetrieveCreditTest()
        {
            var requestTester = GetRequestTester();
            var creditId = "1";
            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.RetrieveCredit(creditId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Address =  string.Format("{0}/credits/{1}",GatewayAdress, creditId),
                    Header = GetDesiredHeader(),
                    Content = null
                }
            );
        }

        [TestMethod]
        public async Task UpdateCreditTest()
        {
            var requestTester = GetRequestTester();
            var creditId = "1";
            var updateCreditRequest = new CreditUpdateRequest() { CreditId = creditId, Description = "new description" };

            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.UpdateCredit(updateCreditRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Address = string.Format("{0}/credits/{1}", GatewayAdress, creditId),
                    Header = GetDesiredHeader(),
                    Content = ToJson(updateCreditRequest)
                }
            );
        }

        [TestMethod]
        public async Task ListCreditsTest()
        {
            var requestTester = GetRequestTester();

            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.ListCredits();
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Address = string.Format("{0}/credits", GatewayAdress),
                    Header = GetDesiredHeader(),
                    Content = null
                }
            );
        }
    }
}
