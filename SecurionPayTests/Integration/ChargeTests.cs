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
    public class ChargeTests : IntegrationTest
    {
        private AddressBuilder _addressBuilder = new AddressBuilder();
        private CustomerRequestBuilder _customerRequestBuilder = new CustomerRequestBuilder();
        private CardRequestBuilder _cardRequestBuilder = new CardRequestBuilder();
        private ChargeRequestBuilder _chargeRequestBuilder = new ChargeRequestBuilder();

        /// <summary>
        /// charge amount exceeds the available fund or the card's credit limit.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ChargeCardWithInsufficientFoundsTest()
        {
            try
            {
                var customerRequest = _customerRequestBuilder.Build();
                var customer = await _gateway.CreateCustomer(customerRequest);

                var chargeRequest = _chargeRequestBuilder.WithCustomerId(customer.Id).WithCard(_cardRequestBuilder).Build(); 
                var charge = await _gateway.CreateCharge(chargeRequest);

            }
            catch (SecurionPayException exc)
            {
                Assert.AreEqual(ErrorCode.InsufficientFunds, exc.Error.Code);
            }
        }

        /// <summary>
        /// charge card with wrong number
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ChargeCardWithWrongNumberTest()
        {
            try
            {
                var customerRequest = _customerRequestBuilder.Build();
                var customer = await _gateway.CreateCustomer(customerRequest);

                var chargeRequest = _chargeRequestBuilder.WithCustomerId(customer.Id).WithCard(_cardRequestBuilder.WithWrongNumber()).Build(); 
                var charge = await _gateway.CreateCharge(chargeRequest);

            }
            catch (SecurionPayException exc)
            {
                Assert.AreEqual(ErrorCode.InvalidNumber, exc.Error.Code);
            }
        }

        /// <summary>
        /// charge exisiting card provided by cardId
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ChargeCardByIdTest()
        {

            var customerRequest = _customerRequestBuilder.Build();
            var customer = await _gateway.CreateCustomer(customerRequest);

            var cardRequest = _cardRequestBuilder.WithCustomerId(customer.Id).Build();
            var card = await _gateway.CreateCard(cardRequest);

            var chargeRequest = _chargeRequestBuilder.WithCustomerId(customer.Id).WithCard(_cardRequestBuilder.WithId(card.Id)).Build();
            var charge = await _gateway.CreateCharge(chargeRequest);

            Assert.AreEqual(2000, charge.Amount);
            Assert.AreEqual(card.Id, charge.Card.Id);
        }

        /// <summary>
        /// charge exisiting card provided by cardId
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ChargeWithShippingAndBillingTest()
        {
            var address = _addressBuilder.Build();

            var customerRequest = _customerRequestBuilder.Build();
            var customer = await _gateway.CreateCustomer(customerRequest);

            var cardRequest = _cardRequestBuilder.WithCustomerId(customer.Id).Build();
            var card = await _gateway.CreateCard(cardRequest);

            var chargeRequest = _chargeRequestBuilder.WithCustomerId(customer.Id)
                                                     .WithCard(_cardRequestBuilder.WithId(card.Id))
                                                     .WithShipping()
                                                     .WithBilling()
                                                     .Build();

            var charge = await _gateway.CreateCharge(chargeRequest);

            Assert.AreEqual(chargeRequest.Shipping.Name, charge.Shipping.Name);
            Assert.AreEqual(chargeRequest.Billing.Name, charge.Billing.Name);
        }

        /// <summary>
        /// charge exisiting card provided by cardId
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ForceToUseThreeDSecureTest()
        {
            try
            {
                var customerRequest = _customerRequestBuilder.Build();
                var customer = await _gateway.CreateCustomer(customerRequest);

                var chargeRequest = _chargeRequestBuilder.WithCustomerId(customer.Id)
                                                         .With3DSecure(new ThreeDSecure() { RequireAttempt = true })
                                                         .WithCard(_cardRequestBuilder.WithCustomerId(customer.Id))
                                                         .Build();
                var charge = await _gateway.CreateCharge(chargeRequest);
            }
            catch (SecurionPayException exc)
            {
                Assert.AreEqual("3D Secure attempt is required.", exc.Error.Message);
            }
        }
    }
}
