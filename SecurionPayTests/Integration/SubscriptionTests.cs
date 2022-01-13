using Xunit;
using SecurionPay.Common;
using SecurionPay.Enums;
using SecurionPay.Exception;
using SecurionPay.Request;
using SecurionPayTests.ModelBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.Integration
{
        public class SubscriptionTests : IntegrationTest
    {
        CustomerRequestBuilder _customerRequestBuilder = new CustomerRequestBuilder();
        CardRequestBuilder _cardRequestBuilder = new CardRequestBuilder();
        ChargeRequestBuilder _chargeRequestBuilder = new ChargeRequestBuilder();
        PlanRequestBuilder _planRequestBuilder = new PlanRequestBuilder();

        [Fact]
        public async Task SubscribeWithNewCardTest()
        {
            try
            {
                var plan = await _gateway.CreatePlan(_planRequestBuilder.Build());

                var customerRequest = _customerRequestBuilder.Build();
                var customer = await _gateway.CreateCustomer(customerRequest);

                var cardRequest = _cardRequestBuilder.Build();
            
                var subscriptionRequest = new SubscriptionRequest() { CustomerId = customer.Id, PlanId = plan.Id,TrialEnd=DateTime.Now.AddDays(10), Card = cardRequest };
                var subscription = await _gateway.CreateSubscription(subscriptionRequest);

                customer = await _gateway.RetrieveCustomer(customer.Id);
                Assert.Equal(cardRequest.CardholderName, customer.Cards.First(c => c.Id == customer.DefaultCardId).CardholderName);

            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        [Fact]
        public async Task CancelSubscribtion()
        {
            try
            {
                // given
                var plan = await _gateway.CreatePlan(_planRequestBuilder.Build());
                var customer = await _gateway.CreateCustomer(_customerRequestBuilder.Build());
                var subscriptionRequest = new SubscriptionRequest() { CustomerId = customer.Id, PlanId = plan.Id, Card = _cardRequestBuilder.Build() };
                var subscription = await _gateway.CreateSubscription(subscriptionRequest);
                // when
                await _gateway.CancelSubscription(new SubscriptionCancelRequest() { SubscriptionId = subscription.Id });
                subscription = await _gateway.RetrieveSubscription(subscription.Id);
                // then
                Assert.True(subscription.Deleted);

            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        [Fact]
        public async Task SubscribeCaptureChargesByDefaultTest()
        {
            try
            {
                var plan = await _gateway.CreatePlan(_planRequestBuilder.Build());

                var customerRequest = _customerRequestBuilder.Build();
                var customer = await _gateway.CreateCustomer(customerRequest);

                var subscriptionRequest = new SubscriptionRequest() { CustomerId = customer.Id, PlanId = plan.Id, TrialEnd = DateTime.Now.AddDays(10) };
                var subscription = await _gateway.CreateSubscription(subscriptionRequest);

                customer = await _gateway.RetrieveCustomer(customer.Id);
                Assert.True(subscription.CaptureCharges);

            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        [Fact]
        public async Task SubscribeWithAdressesTest()
        {
            var address = new AddressBuilder().Build();
            try
            {
                var plan = await _gateway.CreatePlan(_planRequestBuilder.Build());

                var customerRequest = _customerRequestBuilder.Build();
                var customer = await _gateway.CreateCustomer(customerRequest);

                var cardRequest = _cardRequestBuilder.WithCustomerId(customer.Id).Build();

                var subscriptionRequest = new SubscriptionRequest() { CustomerId = customer.Id, PlanId = plan.Id, Card = cardRequest, Billing = new Billing() { Address = address ,Name="name",Vat="123123"} };
                var subscription = await _gateway.CreateSubscription(subscriptionRequest);

                Assert.Equal(subscription.Billing.Address.FirstLine, address.FirstLine);
                Assert.Equal(subscription.Billing.Address.City, address.City);
                Assert.Equal(subscription.Billing.Address.CountryISOCode, address.CountryISOCode);
                Assert.Equal(subscription.Billing.Address.State, address.State);

            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        [Fact]
        public async Task SubscribeWithCardFromChargeTest()
        {
            try
            {
                var plan = await _gateway.CreatePlan(_planRequestBuilder.Build());

                var customerRequest = _customerRequestBuilder.Build();
                var customer = await _gateway.CreateCustomer(customerRequest);

                var chargeRequest = _chargeRequestBuilder.WithCard(_cardRequestBuilder).Build();
                var charge = await _gateway.CreateCharge(chargeRequest);

                var subscriptionRequest = new SubscriptionRequest() { CustomerId = customer.Id, PlanId = plan.Id, Card = new CardRequest() { Id = charge.Id } };
                var subscription = await _gateway.CreateSubscription(subscriptionRequest);

                customer = await _gateway.RetrieveCustomer(customer.Id);
                Assert.Equal(chargeRequest.Card.CardholderName, customer.Cards.First(c => c.Id == customer.DefaultCardId).CardholderName);
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }
    }
}
