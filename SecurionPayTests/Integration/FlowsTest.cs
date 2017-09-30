using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurionPay.Enums;
using SecurionPay.Exception;
using SecurionPay.Request;
using SecurionPay.Response;
using SecurionPayTests.ModelBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurionPayTests.Utils;

namespace SecurionPayTests.Integration
{
    [TestClass]
    public class FlowsTest : IntegrationTest
    {
        private CustomerRequestBuilder _customerRequestBuilder = new CustomerRequestBuilder();
        private CardRequestBuilder _cardRequestBuilder = new CardRequestBuilder();
        private TokenRequestBuilder _tokenRequestBuilder = new TokenRequestBuilder();
        private ChargeRequestBuilder _chargeRequestBuilder = new ChargeRequestBuilder();

        /// <summary>
        /// test for flow Token -> Charge -> Capture -> Refund
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ChargeCaptureRefundFlowTest()
        {
            try
            {
                var createTokenRequest = _tokenRequestBuilder.Build();
                var token = await _gateway.CreateToken(createTokenRequest);
                token = await _gateway.RetrieveToken(token.Id);

                var chargeRequest = _chargeRequestBuilder.WithCard(_cardRequestBuilder.WithId(token.Id))
                                                         .WithIsCaptured(false)
                                                         .Build();
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
                Assert.AreEqual(chargeRequest.Amount - 500, charge.Amount);
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
                var createTokenRequest = _tokenRequestBuilder.Build();
                var token = await _gateway.CreateToken(createTokenRequest);
                token = await _gateway.RetrieveToken(token.Id);

                var chargeRequest = _chargeRequestBuilder.WithCard(_cardRequestBuilder.WithId(token.Id)).Build();
                var charge = await _gateway.CreateCharge(chargeRequest);

                var customerRequest = _customerRequestBuilder.WithCard(_cardRequestBuilder.WithId(charge.Id)).Build(); 
                var customer = await _gateway.CreateCustomer(customerRequest);

                var chargeRequest2 = new ChargeRequestBuilder().WithCustomerId(customer.Id).Build();
                charge = await _gateway.CreateCharge(chargeRequest2);

                Assert.AreEqual(chargeRequest2.Amount, charge.Amount);
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

                var customerRequest = _customerRequestBuilder.Build();
                var customer = await _gateway.CreateCustomer(customerRequest);

                var createTokenRequest = _tokenRequestBuilder.Build();
                var token = await _gateway.CreateToken(createTokenRequest);


                var subscriptionRequest = new SubscriptionRequest() { CustomerId = customer.Id, PlanId = plan.Id, Card = new CardRequest() { Id = token.Id } };
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
                var customerRequest = _customerRequestBuilder.Build();
                var customer = await _gateway.CreateCustomer(customerRequest);

                var chargeRequest = _chargeRequestBuilder.WithCard(_cardRequestBuilder)
                                                         .WithCustomerId(customer.Id)
                                                         .Build();

                var charge = await _gateway.CreateCharge(chargeRequest);

                var chargeRequest2 = new ChargeRequest() { Amount = 1000, Currency = chargeRequest.Currency, CustomerId = charge.CustomerId };
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
                var customerRequest = _customerRequestBuilder.Build();
                var customer = await _gateway.CreateCustomer(customerRequest);

                var cardRequest = _cardRequestBuilder.WithCustomerId(customer.Id).Build();
                var card = await _gateway.CreateCard(cardRequest);

                card = await _gateway.RetrieveCard(customer.Id, card.Id);

                Assert.AreEqual(cardRequest.GetLast4(), card.Last4);
                Assert.AreEqual(cardRequest.ExpMonth, card.ExpMonth);
                Assert.AreEqual(cardRequest.ExpYear, card.ExpYear);
                Assert.AreEqual(cardRequest.CardholderName, card.CardholderName);

                var chargeReqest = _chargeRequestBuilder.WithCustomerId(customer.Id)
                                                        .Build();
                var charge =await _gateway.CreateCharge(chargeReqest);

                Assert.AreEqual(chargeReqest.Amount, charge.Amount);
                
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

    }
}
