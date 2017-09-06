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
        AddressBuilder _addressBuilder = new AddressBuilder();

        /// <summary>
        /// charge amount exceeds the available fund or the card's credit limit.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ChargeCardWithInsufficientFoundsTest()
        {
            try
            {
                var customerRequest = new CustomerRequest() { Email = GetRandomEmail(), Description = "test customer" };
                var customer = await _gateway.CreateCustomer(customerRequest);

                var cardRequest = new CardRequest() { Number = "4024007118468684", ExpMonth = "12", ExpYear = "2055", Cvc = "123" };
                var chargeRequest = new ChargeRequest() { Amount = 2000, CurrencyISOCode = "EUR", CustomerId = customer.Id, Card = cardRequest };
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
                var customerRequest = new CustomerRequest() { Email = GetRandomEmail(), Description = "test customer" };
                var customer = await _gateway.CreateCustomer(customerRequest);

                var cardRequest = new CardRequest() { Number = "44444444", ExpMonth = "12", ExpYear = "2055", Cvc = "123" };
                var chargeRequest = new ChargeRequest() { Amount = 2000, CurrencyISOCode = "EUR", CustomerId = customer.Id, Card = cardRequest };
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

            var customerRequest = new CustomerRequest() { Email = GetRandomEmail(), Description = "test customer" };
            var customer = await _gateway.CreateCustomer(customerRequest);

            var cardRequest = new CardRequest() { CustomerId = customer.Id, Number = "4242424242424242", ExpMonth = "12", ExpYear = "2055", Cvc = "123", CardholderName = "test test" };
            var card = await _gateway.CreateCard(cardRequest);

            var chargeRequest = new ChargeRequest() { Amount = 2000, CurrencyISOCode = "EUR", CustomerId = customer.Id, Card = new CardRequest() { Id = card.Id } };
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

            var customerRequest = new CustomerRequest() { Email = GetRandomEmail(), Description = "test customer" };
            var customer = await _gateway.CreateCustomer(customerRequest);

            var cardRequest = new CardRequest() { CustomerId = customer.Id, Number = "4242424242424242", ExpMonth = "12", ExpYear = "2055", Cvc = "123", CardholderName = "test test" };
            var card = await _gateway.CreateCard(cardRequest);

            var chargeRequest = new ChargeRequest()
            {
                Amount = 2000,
                CurrencyISOCode = "EUR",
                CustomerId = customer.Id,
                Card = new CardRequest() { Id = card.Id},
                Shipping = new Shipping() { Name = "shipping name", Address = address },
                Billing = new Billing() { Name = "Billing name", Address = address ,Vat="76663827374"}
            };
            var charge = await _gateway.CreateCharge(chargeRequest);

            Assert.AreEqual("shipping name", charge.Shipping.Name);
            Assert.AreEqual("Billing name", charge.Billing.Name);
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
                var customerRequest = new CustomerRequest() { Email = GetRandomEmail(), Description = "test customer" };
                var customer = await _gateway.CreateCustomer(customerRequest);

                var cardRequest = new CardRequest() { CustomerId = customer.Id, Number = "4242424242424242", ExpMonth = "12", ExpYear = "2055", Cvc = "123", CardholderName = "test test" };

                var chargeRequest = new ChargeRequest() { Amount = 2000, CurrencyISOCode = "EUR", CustomerId = customer.Id, Card = cardRequest, ThreeDSecure = new ThreeDSecure() { RequireAttempt = true } };
                var charge = await _gateway.CreateCharge(chargeRequest);
            }
            catch (SecurionPayException exc)
            {
                Assert.AreEqual("3D Secure attempt is required.", exc.Error.Message);

            }
        }
    }
}
