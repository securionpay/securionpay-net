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
    public class CardsTests:BaseUnitTestsSet
    {
        [TestMethod]
        public async Task CreateCardTest()
        {
            var requestTester = GetRequestTester();
            var customerId = "1";
            var cardRequest = new CardRequest() { CustomerId= customerId, Number="404129331232" , ExpMonth="6" , ExpYear="2015" , CardholderName="John Smith" };
            await requestTester.TestMethod<Card>(
                async (api) =>
                {
                    await api.CreateCard(cardRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Action = string.Format("customers/{0}/cards", customerId),
                    Parameter = cardRequest
                }
            );
        }

        [TestMethod]
        public async Task RetrieveCardTest()
        {
            var requestTester = GetRequestTester();
            var customerId = "1";
            var cardId = "1";
            await requestTester.TestMethod<Card>(
                async (api) =>
                {
                    await api.RetrieveCard(customerId, cardId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Action =  string.Format("customers/{0}/cards/{1}", customerId,cardId),
                    Parameter = null
                }
            );
        }


        [TestMethod]
        public async Task UpdateCardTest()
        {
            var requestTester = GetRequestTester();
            var customerId = "1";
            var cardId = "1";
            var cardUpdateRequest = new CardUpdateRequest() {CardholderName="Jan Kowaslki",CustomerId= customerId ,CardId=cardId};
            await requestTester.TestMethod<Card>(
                async (api) =>
                {
                    await api.UpdateCard(cardUpdateRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Action = string.Format("customers/{0}/cards/{1}", customerId, cardId),
                    Parameter = cardUpdateRequest
                }
            );
        }

        [TestMethod]
        public async Task DeleteCardTest()
        {
            var requestTester = GetRequestTester();
            var customerId = "1";
            var cardId = "1";
            await requestTester.TestMethod<DeleteResponse>(
                async (api) =>
                {
                    await api.DeleteCard(customerId, cardId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Delete,
                    Action = string.Format("customers/{0}/cards/{1}", customerId, cardId),
                    Parameter = null
                }
            );
        }

        [TestMethod]
        public async Task ListCardsTest()
        {
            var requestTester = GetRequestTester();
            var customerId = "1";
            var cardListRequest = new CardListRequest() { CustomerId= customerId,Limit=5 };
            await requestTester.TestMethod<SecurionpayList>(
                async (api) =>
                {
                    await api.ListCards(cardListRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Action = string.Format("customers/{0}/cards?limit=5", customerId),
                    Parameter = null
                }
            );
        }

    }
}
