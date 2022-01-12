using Xunit;
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
        public class CardsTests:BaseUnitTestsSet
    {
        private CardRequestBuilder _cardRequestBuilder = new CardRequestBuilder();

        [Fact]
        public async Task CreateCardTest()
        {
            var requestTester = GetRequestTester();
            var customerId = "1";
            var cardRequest = _cardRequestBuilder.WithCustomerId(customerId).Build();
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

        [Fact]
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


        [Fact]
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

        [Fact]
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

        [Fact]
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
