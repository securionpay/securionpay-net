using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class CustomerTests : IntegrationTest
    {
        [TestMethod]
        public async Task CustomerWithNewCardTest()
        {
            try
            {
                var cardRequest = new CardRequest() { Number = "4242424242424242", ExpMonth = "12", ExpYear = "2055", Cvc = "123", CardholderName = "test test" };

                var customerRequest = new CustomerWithNewCardRequest() { Email = GetRandomEmail(), Description = "test customer",Card=cardRequest };
                var customer = await _gateway.CreateCustomer(customerRequest);

                Assert.AreEqual(1, customer.Cards.Count);
                Assert.AreEqual("test test", customer.Cards.First().CardholderName);

            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        [TestMethod]
        public async Task CustomerWithCardTokenTest()
        {
            try
            {
                var createTokenRequest = new TokenRequest() { Number = "4012000100000007", ExpMonth = "11", ExpYear = CorrectCardExpiryYear, Cvc = "432", CardholderName = "John Smith" };
                var token = await _gateway.CreateToken(createTokenRequest);
                token = await _gateway.RetrieveToken(token.Id);

                var customerRequest = new CustomerWithCardTokenRequest() { Email = GetRandomEmail(), Description = "test customer", CardToken = token.Id };
                var customer = await _gateway.CreateCustomer(customerRequest);


                Assert.AreEqual(1, customer.Cards.Count);
                Assert.AreEqual("John Smith", customer.Cards.First().CardholderName);

            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }
    }
}
