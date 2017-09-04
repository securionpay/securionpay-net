using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurionPay.Enums;
using SecurionPay.Exception;
using SecurionPay.Request;
using SecurionPay.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurionPayTests.Integration
{
    [TestClass]
    public class FlowsTest : IntegrationTest
    {
        /// <summary>
        /// test for flow Token -> Charge -> Capture -> Refund
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ChargeCaptureRefundFlowTest()
        {
            try
            {
                var createTokenRequest = new TokenRequest() { Number = "4012000100000007", ExpMonth = "11", ExpYear = CorrectCardExpiryYear, Cvc = "432", CardholderName = "John Smith" };
                var token = await _gateway.CreateToken(createTokenRequest);
                token = await _gateway.RetrieveToken(token.Id);

                var chargeRequest = new ChargeRequest() { Amount = 1000, CurrencyISOCode = "PLN", Card = ChargeCardDefinition.FromCardToken(token.Id), Description = "sss", Captured = false };
                var charge = await _gateway.CreateCharge(chargeRequest);

                var capture = new CaptureRequest() { ChargeId = charge.Id };
                charge = await _gateway.CaptureCharge(capture);

                var refund = new RefundRequest() { ChargeId = charge.Id, Amount = 500 };
                charge = await _gateway.RefundCharge(refund);

                charge = await _gateway.RetrieveCharge(charge.Id);

                Assert.IsTrue(charge.Captured);
                Assert.IsTrue(charge.Refunded);
                Assert.AreEqual(1, charge.Refunds.Count);
                Assert.AreEqual(500, charge.Refunds.First().Amount);
                Assert.IsFalse(charge.Refunds.First().Deleted);
                Assert.AreEqual(500, charge.Amount);
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }

        }


        /// <summary>
        /// test for flow Token -> Charge -> Customer -> Charge (existing card)
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ChargeCustomerFromChargeFlowTest()
        {
            try
            {
                var createTokenRequest = new TokenRequest() { Number = "4012000100000007", ExpMonth = "11", ExpYear = CorrectCardExpiryYear, Cvc = "432", CardholderName = "John Smith" };
                var token = await _gateway.CreateToken(createTokenRequest);
                token = await _gateway.RetrieveToken(token.Id);

                var chargeRequest = new ChargeRequest() { Amount = 1000, CurrencyISOCode = "PLN", Card = ChargeCardDefinition.FromCardToken( token.Id ), Description = "sss" };
                var charge = await _gateway.CreateCharge(chargeRequest);

                var customerRequest = new CustomerWithCardFromChargeRequest() { Email = GetRandomEmail(), Description = "test customer", ChargeId =charge.Id };
                var customer = await _gateway.CreateCustomer(customerRequest);

                var chargeRequest2 = new ChargeRequest() { Amount = 1000, CurrencyISOCode = "PLN", CustomerId = customer.Id, Description = "sss" };
                charge = await _gateway.CreateCharge(chargeRequest2);

                Assert.AreEqual(1000, charge.Amount);
                Assert.AreEqual(customer.Id, charge.CustomerId);

            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        /// <summary>
        /// test for flow Plan -> Customer -> Token -> Subscription
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task SubscribeWithTokenTest()
        {
            try
            {
                var planRequest = new PlanRequest() { Amount = 1000, Currency = "EUR", Interval = Interval.Month, Name = "Test plan" + _random.Next(999) };
                var plan = await _gateway.CreatePlan(planRequest);

                var customerRequest = new CustomerRequest() { Email = GetRandomEmail(), Description = "test customer" };
                var customer = await _gateway.CreateCustomer(customerRequest);

                var createTokenRequest = new TokenRequest() { Number = "4012000100000007", ExpMonth = "11", ExpYear = CorrectCardExpiryYear, Cvc = "432", CardholderName = "John Smith" };
                var token = await _gateway.CreateToken(createTokenRequest);


                var subscriptionRequest = new SubscriptionWithCardTokenRequest() { CustomerId = customer.Id, PlanId = plan.Id, CardToken = token.Id  };
                var subscription = await _gateway.CreateSubscription(subscriptionRequest);

                Assert.AreEqual(plan.Id, subscription.PlanId);
                Assert.AreEqual(customer.Id, subscription.CustomerId);

            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        /// <summary>
        /// test for flow Customer -> Charge -> Charge (existing card)
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ChargeCustomerTwiceTest()
        {
            try
            {
                var customerRequest = new CustomerRequest() { Email = GetRandomEmail(), Description = "test customer" };
                var customer = await _gateway.CreateCustomer(customerRequest);

                var cardRequest = new CardRequest() { Number = "4242424242424242", ExpMonth = "12", ExpYear = "2055", Cvc = "123" };
                var chargeRequest = new ChargeRequest() { Amount = 2000, CurrencyISOCode = "EUR", CustomerId=customer.Id,Card= ChargeCardDefinition.NewCard(cardRequest)};
                var charge = await _gateway.CreateCharge(chargeRequest);

                var chargeRequest2 = new ChargeRequest(){Amount=1000,CurrencyISOCode="EUR",CustomerId=charge.CustomerId};
                var charge2 = await _gateway.CreateCharge(chargeRequest2);

                Assert.AreEqual(1000,charge2.Amount);
                Assert.AreEqual(customer.Id, charge2.CustomerId);

            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        /// <summary>
        /// test for flow Customer -> Add card -> Charge card
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ChargeCardTest()
        {
            try
            {
                var customerRequest = new CustomerRequest() { Email = GetRandomEmail(), Description = "test customer" };
                var customer = await _gateway.CreateCustomer(customerRequest);

                var cardRequest = new CardRequest() { CustomerId = customer.Id, Number = "4242424242424242", ExpMonth = "12", ExpYear = "2055", Cvc = "123", CardholderName = "test test" };
                var card = await _gateway.CreateCard(cardRequest);

                card = await _gateway.RetrieveCard(customer.Id, card.Id);

                Assert.AreEqual("4242", card.Last4);
                Assert.AreEqual("12", card.ExpMonth);
                Assert.AreEqual("2055", card.ExpYear);
                Assert.AreEqual("test test", card.CardholderName);

                var chargeReqest = new ChargeRequest() { Amount=1000,CurrencyISOCode="EUR",CustomerId=card.CustomerId};
                var charge =await _gateway.CreateCharge(chargeReqest);

                Assert.AreEqual(1000, charge.Amount);
                
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

    }
}
