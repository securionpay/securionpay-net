using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurionPayTests.Units.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SecurionPay.Request;

namespace SecurionPayTests.Units
{
    [TestClass]
    public class SubscriptionsTests:BaseUnitTestsSet
    {
        [TestMethod]
        public async Task CreateSubscriptionWithCardTest()
        {
            var requestTester = new RequestTester(PrivateKey, GatewayAdress);
            var customerId = "1";
            var cardRequest = new CardRequest() { Number = "404129331232", ExpMonth = "6", ExpYear = "2015", CardholderName = "Jan Kowalski" };
            var subscriptionRequest = new SubscriptionRequest() { CustomerId= customerId ,Card= cardRequest,Quantity=1000,PlanId="1" };
            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.CreateSubscription(subscriptionRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Address = string.Format("{0}/customers/{1}/subscriptions", GatewayAdress, customerId),
                    Header = GetDesiredHeader(),
                    Content = ToJson(subscriptionRequest)
                }
            );
        }

        [TestMethod]
        public async Task CreateSubscriptionWithTokenTest()
        {
            var requestTester = new RequestTester(PrivateKey, GatewayAdress);
            var customerId = "1";
            var tokenId = "1";
            var subscriptionRequest = new SubscriptionRequest() { CustomerId = customerId, Card = new CardRequest() { Id = tokenId }, Quantity = 1000, PlanId = "1" };
            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.CreateSubscription(subscriptionRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Address = string.Format("{0}/customers/{1}/subscriptions", GatewayAdress, customerId),
                    Header = GetDesiredHeader(),
                    Content = ToJson(subscriptionRequest)
                }
            );
        }


        [TestMethod]
        public async Task RetrieveSubscriptionTest()
        {
            var requestTester = new RequestTester(PrivateKey, GatewayAdress);
            var customerId = "c1";
            var subscriptionId = "s1";
            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.RetrieveSubscription(customerId,subscriptionId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Address = string.Format("{0}/customers/{1}/subscriptions/{2}", GatewayAdress, customerId, subscriptionId),
                    Header = GetDesiredHeader(),
                    Content = null
                }
            );
        }

        [TestMethod]
        public async Task UpdateSubscriptionTest()
        {
            var requestTester = new RequestTester(PrivateKey, GatewayAdress);
            var customerId = "c1";
            var subscriptionId = "s1";
            var cardRequest = new CardRequest() { Number = "404129331232", ExpMonth = "6", ExpYear = "2015", CardholderName = "Jan Kowalski" };
            var subscriptionUpdateRequest = new SubscriptionUpdateRequest() { CustomerId= customerId ,Card= cardRequest ,Quantity=1000,SubscriptionId= subscriptionId };
            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.UpdateSubscription(subscriptionUpdateRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Address = string.Format("{0}/customers/{1}/subscriptions/{2}", GatewayAdress, customerId, subscriptionId),
                    Header = GetDesiredHeader(),
                    Content = ToJson(subscriptionUpdateRequest)
                }
            );
        }

        [TestMethod]
        public async Task CancelSubscriptionTest()
        {
            var requestTester = new RequestTester(PrivateKey, GatewayAdress);
            var customerId = "c1";
            var subscriptionId = "s1";
            var cardRequest = new CardRequest() { Number = "404129331232", ExpMonth = "6", ExpYear = "2015", CardholderName = "Jan Kowalski" };
            var subscriptionCancelRequest = new SubscriptionCancelRequest() { CustomerId = customerId,SubscriptionId = subscriptionId ,AtPeriodEnd=true};
            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.CancelSubscription(subscriptionCancelRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Delete,
                    Address = string.Format("{0}/customers/{1}/subscriptions/{2}", GatewayAdress, customerId, subscriptionId),
                    Header = GetDesiredHeader(),
                    Content = ToJson(subscriptionCancelRequest)
                }
            );
        }

        [TestMethod]
        public async Task ListSubscriptionsTest()
        {
            var requestTester = new RequestTester(PrivateKey, GatewayAdress);
            var customerId = "c1";
            await requestTester.TestMethod(
                async (api) =>
                {
                    await api.ListSubscriptions(customerId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Address = string.Format("{0}/customers/{1}/subscriptions", GatewayAdress, customerId),
                    Header = GetDesiredHeader(),
                    Content = null
                }
            );
        }

    }
}
