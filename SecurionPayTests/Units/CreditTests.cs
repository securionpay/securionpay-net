using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurionPay.Request;
using SecurionPay.Response;
using SecurionPayTests.ModelBuilders;
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
        private CardRequestBuilder _cardRequestBuilder = new CardRequestBuilder();

        [TestMethod]
        public async Task CreateCreditTest()
        {
            var requestTester = GetRequestTester();
            var customerId = "1";
            var creditRequest = new CreditRequest() { CustomerId = customerId, Card = _cardRequestBuilder.WithId("1").Build(), Amount=100,Currency="EUR"};
            await requestTester.TestMethod<Credit>(
                async (api) =>
                {
                    await api.CreateCredit(creditRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Action = "credits",
                    Parameter = creditRequest
                }
            );
        }

        [TestMethod]
        public async Task CreateCreditWithCartdTest()
        {
            var requestTester = GetRequestTester();
            var customerId = "1";
            var creditRequest = new CreditRequest()
            {
                CustomerId = customerId,
                Card = _cardRequestBuilder.Build(),
                Amount = 100,
                Currency = "EUR"
            };
            await requestTester.TestMethod<Credit>(
                async (api) =>
                {
                    await api.CreateCredit(creditRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Action = "credits",
                    Parameter = creditRequest
                }
            );
        }

        [TestMethod]
        public async Task RetrieveCreditTest()
        {
            var requestTester = GetRequestTester();
            var creditId = "1";
            await requestTester.TestMethod<Credit>(
                async (api) =>
                {
                    await api.RetrieveCredit(creditId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Action = string.Format("credits/{0}", creditId),
                    Parameter = null
                }
            );
        }

        [TestMethod]
        public async Task UpdateCreditTest()
        {
            var requestTester = GetRequestTester();
            var creditId = "1";
            var updateCreditRequest = new CreditUpdateRequest() { CreditId = creditId, Description = "new description" };

            await requestTester.TestMethod<Credit>(
                async (api) =>
                {
                    await api.UpdateCredit(updateCreditRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Action = string.Format("credits/{0}", creditId),
                    Parameter = updateCreditRequest
                }
            );
        }

        [TestMethod]
        public async Task ListCreditsTest()
        {
            var requestTester = GetRequestTester();

            await requestTester.TestMethod<SecurionpayList>(
                async (api) =>
                {
                    await api.ListCredits();
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Action = "credits",
                    Parameter = null
                }
            );
        }
    }
}
