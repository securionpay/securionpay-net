using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurionPay.Enums;
using SecurionPay.Exception;
using SecurionPay.Request;
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
                var chargeRequest = new ChargeRequest() { Amount = 2000, Currency = "EUR", CustomerId = customer.Id, Card = cardRequest };
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
                var chargeRequest = new ChargeRequest() { Amount = 2000, Currency = "EUR", CustomerId = customer.Id, Card = cardRequest };
                var charge = await _gateway.CreateCharge(chargeRequest);

            }
            catch (SecurionPayException exc)
            {
                Assert.AreEqual(ErrorCode.InvalidNumber, exc.Error.Code);
            }
        }
    }
}
