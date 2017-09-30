using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class CustomerTests : IntegrationTest
    {
        CustomerRequestBuilder _customerRequestBuilder = new CustomerRequestBuilder();
        CardRequestBuilder _cardRequestBuilder = new CardRequestBuilder();
        TokenRequestBuilder _tokenRequestBuilder = new TokenRequestBuilder();

        [TestMethod]
        public async Task CustomerWithNewCardTest()
        {
            try
            {
                var customerRequest = _customerRequestBuilder.WithCard(_cardRequestBuilder).Build();
                var customer = await _gateway.CreateCustomer(customerRequest);

                Assert.AreEqual(1, customer.Cards.Count);
                Assert.AreEqual(customerRequest.Card.CardholderName, customer.Cards.First().CardholderName);

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
                var createTokenRequest = _tokenRequestBuilder.Build();
                var token = await _gateway.CreateToken(createTokenRequest);
                token = await _gateway.RetrieveToken(token.Id);

                var customerRequest = _customerRequestBuilder.WithCard(_cardRequestBuilder.WithId(token.Id)).Build();
                var customer = await _gateway.CreateCustomer(customerRequest);

                Assert.AreEqual(1, customer.Cards.Count);
                Assert.AreEqual(createTokenRequest.CardholderName, customer.Cards.First().CardholderName);

            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }
    }
}
