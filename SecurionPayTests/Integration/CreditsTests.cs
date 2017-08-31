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
    public class CreditsTests : IntegrationTest
    {
        /// <summary>
        /// test for creating and listing credits
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task CreateCreditWithTokenAndListCreditsTest()
        {
            try
            {
                var createTokenRequest = new TokenRequest() { Number = "4012000100000007", ExpMonth = "11", ExpYear = CorrectCardExpiryYear, Cvc = "432", CardholderName = "John Smith" };
                var token = await _gateway.CreateToken(createTokenRequest);

                var creditRequest = new CreditRequest()
                {
                    Amount = 100,
                    Currency = "EUR",
                    Description = "desc",
                    CardId = token.Id
                };
                var newCredit = await _gateway.CreateCredit(creditRequest);
                var credits = await _gateway.ListCredits();
                Assert.IsNotNull(credits.List.FirstOrDefault(c => c.Id == newCredit.Id));
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        /// <summary>
        /// test for creating and retireving credits
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task CreateCreditWithTokenAndRetireveCreditTest()
        {
            try {
                var createTokenRequest = new TokenRequest() { Number = "4012000100000007", ExpMonth = "11", ExpYear = CorrectCardExpiryYear, Cvc = "432", CardholderName = "John Smith" };
                var token = await _gateway.CreateToken(createTokenRequest);

                var creditRequest = new CreditRequest()
                {
                    Amount = 100,
                    Currency = "EUR",
                    Description = "desc",
                    CardId = token.Id
                };
                var newCredit = await _gateway.CreateCredit(creditRequest);
                var retrievedCredit = await _gateway.RetrieveCredit(newCredit.Id);
                Assert.AreEqual(creditRequest.Amount, retrievedCredit.Amount);
                Assert.AreEqual(creditRequest.Currency, retrievedCredit.Currency);
                Assert.AreEqual(creditRequest.Description, retrievedCredit.Description);
                Assert.AreEqual(createTokenRequest.CardholderName, retrievedCredit.Card.CardholderName);
                Assert.AreEqual(createTokenRequest.ExpMonth, retrievedCredit.Card.ExpMonth);
                Assert.AreEqual(createTokenRequest.ExpYear, retrievedCredit.Card.ExpYear);
                Assert.AreEqual("0007", retrievedCredit.Card.Last4);
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        /// <summary>
        /// test for creating credit with cardId and customerId
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task CreateWithCardIdAndCustomerIdTest()
        {
            try
            {
                var customerRequest = new CustomerRequest() { Email = GetRandomEmail(), Description = "test customer" };
                var customer = await _gateway.CreateCustomer(customerRequest);

                var cardRequest = new CardRequest() { CustomerId = customer.Id, Number = "4242424242424242", ExpMonth = "12", ExpYear = "2055", Cvc = "123", CardholderName = "test test" };
                var card = await _gateway.CreateCard(cardRequest);

                var creditRequest = new CreditRequest()
                {
                    Amount = 100,
                    Currency = "EUR",
                    Description = "desc",
                    CardId = card.Id,
                    CustomerId = customer.Id
                };
                var newCredit = await _gateway.CreateCredit(creditRequest);
                var retrievedCredit = await _gateway.RetrieveCredit(newCredit.Id);
                Assert.AreEqual(creditRequest.CustomerId, retrievedCredit.CustomerId);

                Assert.AreEqual(card.Id, retrievedCredit.Card.Id);
                Assert.AreEqual(cardRequest.CardholderName, retrievedCredit.Card.CardholderName);
                Assert.AreEqual(cardRequest.ExpMonth, retrievedCredit.Card.ExpMonth);
                Assert.AreEqual(cardRequest.ExpYear, retrievedCredit.Card.ExpYear);
                Assert.AreEqual("4242", retrievedCredit.Card.Last4);
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        /// <summary>
        /// test for creating credit with card details
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task CreateWithCardDetailsTest()
        {
            try
            {
                var customerRequest = new CustomerRequest() { Email = GetRandomEmail(), Description = "test customer" };
                var customer = await _gateway.CreateCustomer(customerRequest);

                var cardRequest = new CardRequest() { Number = "4242424242424242", ExpMonth = "12", ExpYear = "2055", Cvc = "123", CardholderName = "test test" };

                var creditRequest = new CreditWithCardRequest()
                {
                    Amount = 100,
                    Currency = "EUR",
                    Description = "desc",
                    Card = cardRequest,
                    CustomerId = customer.Id
                };
                var newCredit = await _gateway.CreateCredit(creditRequest);
                var retrievedCredit = await _gateway.RetrieveCredit(newCredit.Id);
                Assert.AreEqual(creditRequest.CustomerId, retrievedCredit.CustomerId);
                Assert.AreEqual(cardRequest.CardholderName, retrievedCredit.Card.CardholderName);
                Assert.AreEqual(cardRequest.ExpMonth, retrievedCredit.Card.ExpMonth);
                Assert.AreEqual(cardRequest.ExpYear, retrievedCredit.Card.ExpYear);
                Assert.AreEqual("4242", retrievedCredit.Card.Last4);
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }

        /// <summary>
        /// test for updatingCredits
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task UpdateCreditTest()
        {
            try
            {
                var createTokenRequest = new TokenRequest() { Number = "4012000100000007", ExpMonth = "11", ExpYear = CorrectCardExpiryYear, Cvc = "432", CardholderName = "John Smith" };
                var token = await _gateway.CreateToken(createTokenRequest);

                var creditRequest = new CreditRequest()
                {
                    Amount = 100,
                    Currency = "EUR",
                    Description = "desc",
                    CardId = token.Id
                };
                var newCredit = await _gateway.CreateCredit(creditRequest);

                var creditUpdateRequest = new CreditUpdateRequest()
                {
                    CreditId = newCredit.Id,
                    Description = "new description"
                };
                var updatedCredit = await _gateway.UpdateCredit(creditUpdateRequest);
                Assert.AreEqual(creditUpdateRequest.Description, updatedCredit.Description);
            }
            catch (SecurionPayException exc)
            {
                HandleApiException(exc);
            }
        }
    }
}
