﻿using Xunit;
using SecurionPayTests.Units.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SecurionPay.Request;
using SecurionPay.Response;
using SecurionPayTests.ModelBuilders;

namespace SecurionPayTests.Units
{
        public class SubscriptionsTests:BaseUnitTestsSet
    {
        private CardRequestBuilder _cardRequestBuilder = new CardRequestBuilder();

        [Fact]
        public async Task CreateSubscriptionWithCardTest()
        {
            var requestTester = GetRequestTester();
            var customerId = "1";
            var cardRequest = _cardRequestBuilder.Build();
            var subscriptionRequest = new SubscriptionRequest() { CustomerId= customerId ,Card= cardRequest,Quantity=1000,PlanId="1" };
            await requestTester.TestMethod<Subscription>(
                async (api) =>
                {
                    await api.CreateSubscription(subscriptionRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Action = "subscriptions",
                    Parameter = subscriptionRequest
                }
            );
        }

        [Fact]
        public async Task CreateSubscriptionWithTokenTest()
        {
            var requestTester = GetRequestTester();
            var customerId = "1";
            var tokenId = "1";
            var subscriptionRequest = new SubscriptionRequest() { CustomerId = customerId, Card = new CardRequest() { Id = tokenId }, Quantity = 1000, PlanId = "1" };
            await requestTester.TestMethod<Subscription>(
                async (api) =>
                {
                    await api.CreateSubscription(subscriptionRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Action = "subscriptions",
                    Parameter = subscriptionRequest
                }
            );
        }


        [Fact]
        public async Task RetrieveSubscriptionTest()
        {
            var requestTester = GetRequestTester();
            var subscriptionId = "s1";
            await requestTester.TestMethod<Subscription>(
                async (api) =>
                {
                    await api.RetrieveSubscription(subscriptionId);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Action = string.Format("subscriptions/{0}", subscriptionId),
                    Parameter = null
                }
            );
        }

        [Fact]
        public async Task UpdateSubscriptionTest()
        {
            var requestTester = GetRequestTester();
            var subscriptionId = "s1";
            var cardRequest = _cardRequestBuilder.Build();
            var subscriptionUpdateRequest = new SubscriptionUpdateRequest() { Card= cardRequest ,Quantity=1000,SubscriptionId= subscriptionId };
            await requestTester.TestMethod<Subscription>(
                async (api) =>
                {
                    await api.UpdateSubscription(subscriptionUpdateRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Post,
                    Action = string.Format("subscriptions/{0}", subscriptionId),
                    Parameter = subscriptionUpdateRequest
                }
            );
        }

        [Fact]
        public async Task CancelSubscriptionTest()
        {
            var requestTester = GetRequestTester();
            var subscriptionId = "s1";
            var cardRequest = _cardRequestBuilder.Build();
            var subscriptionCancelRequest = new SubscriptionCancelRequest() { SubscriptionId = subscriptionId ,AtPeriodEnd=true};
            await requestTester.TestMethod<Subscription>(
                async (api) =>
                {
                    await api.CancelSubscription(subscriptionCancelRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Delete,
                    Action = string.Format("subscriptions/{0}", subscriptionId),
                    Parameter = subscriptionCancelRequest
                }
            );
        }

        [Fact]
        public async Task ListSubscriptionsTest()
        {
            var requestTester = GetRequestTester();
            var customerId = "c1";
            var listRequest  = new SubscriptionListRequest() { CustomerId = customerId };
            await requestTester.TestMethod<SecurionpayList>(
                async (api) =>
                {
                    await api.ListSubscriptions(listRequest);
                },
                new RequestDescriptor()
                {
                    Method = HttpMethod.Get,
                    Action = "subscriptions?customerId=" + customerId,
                    Parameter = null
                }
            );
        }

    }
}
