using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    [TestClass]
    public class SubscriptionTests : IntegrationTest
    {
        [TestMethod]
        public async Task SubscribeWithNewCardTest()
        {
            try
            {
                var planRequest = new PlanRequest() { Amount = 1000, Currency = "EUR", Interval = Interval.Month, Name = "Test plan" + _random.Next(999) };
                var plan = await _gateway.CreatePlan(planRequest);

                var customerRequest = new CustomerRequest() { Email = GetRandomEmail(), Description = "test customer" };
                var customer = await _gateway.CreateCustomer(customerRequest);

                var cardRequest = new CardRequest() { CustomerId = customer.Id, Number = "4242424242424242", ExpMonth = "12", ExpYear = "2055", Cvc = "123", CardholderName = "test cardholder" };
            
                var subscriptionRequest = new SubscriptionRequest() { CustomerId = customer.Id, PlanId = plan.Id, Card = cardRequest };
                var subscription = await _gateway.CreateSubscription(subscriptionRequest);

                customer = await _gateway.RetrieveCustomer(customer.Id);
                Assert.AreEqual("test cardholder", customer.Cards.First(c => c.Id == customer.DefaultCardId).CardholderName);

            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        [TestMethod]
        public async Task SubscribeWithAdressesTest()
        {
            var address = new AddressBuilder().Build();
            try
            {
                var planRequest = new PlanRequest() { Amount = 1000, Currency = "EUR", Interval = Interval.Month, Name = "Test plan" + _random.Next(999) };
                var plan = await _gateway.CreatePlan(planRequest);

                var customerRequest = new CustomerRequest() { Email = GetRandomEmail(), Description = "test customer" };
                var customer = await _gateway.CreateCustomer(customerRequest);

                var cardRequest = new CardRequest() { CustomerId = customer.Id, Number = "4242424242424242", ExpMonth = "12", ExpYear = "2055", Cvc = "123", CardholderName = "test cardholder" };

                var subscriptionRequest = new SubscriptionRequest() { CustomerId = customer.Id, PlanId = plan.Id, Card = cardRequest, Billing = new Billing() { Address = address ,Name="name",Vat="123123"} };
                var subscription = await _gateway.CreateSubscription(subscriptionRequest);

                Assert.AreEqual(subscription.Billing.Address.FirstLine, address.FirstLine);
                Assert.AreEqual(subscription.Billing.Address.City, address.City);
                Assert.AreEqual(subscription.Billing.Address.CountryISOCode, address.CountryISOCode);
                Assert.AreEqual(subscription.Billing.Address.State, address.State);

            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        [TestMethod]
        public async Task SubscribeWithCardFromChargeTest()
        {
            try
            {
                var planRequest = new PlanRequest() { Amount = 1000, Currency = "EUR", Interval = Interval.Month, Name = "Test plan" + _random.Next(999) };
                var plan = await _gateway.CreatePlan(planRequest);

                var customerRequest = new CustomerRequest() { Email = GetRandomEmail(), Description = "test customer" };
                var customer = await _gateway.CreateCustomer(customerRequest);

                var cardRequest = new CardRequest() { Number = "4242424242424242", ExpMonth = "12", ExpYear = "2055", Cvc = "123",CardholderName="charge cardholder name" };
                var chargeRequest = new ChargeRequest() { Amount = 2000, Currency = "EUR", Card =cardRequest };
                var charge = await _gateway.CreateCharge(chargeRequest);

                var subscriptionRequest = new SubscriptionRequest() { CustomerId = customer.Id, PlanId = plan.Id, Card = new CardRequest() { Id = charge.Id } };
                var subscription = await _gateway.CreateSubscription(subscriptionRequest);

                customer = await _gateway.RetrieveCustomer(customer.Id);
                Assert.AreEqual("charge cardholder name", customer.Cards.First(c => c.Id == customer.DefaultCardId).CardholderName);
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }
    }
}
